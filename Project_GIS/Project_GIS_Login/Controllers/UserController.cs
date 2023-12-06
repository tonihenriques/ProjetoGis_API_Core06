using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Business.Concrect;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.ViewModels;

namespace Project_GIS_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        protected IUserBusiness _userBusiness;
        protected IUserRolesBusiness _userRolesBusiness;
        protected IRolesBusiness _rolesBusiness;

        public UserController(IUserBusiness userBusiness,
            IUserRolesBusiness userRolesBusiness,
            IRolesBusiness rolesBusiness)
        {
            _userBusiness = userBusiness;
            _userRolesBusiness = userRolesBusiness;
            _rolesBusiness = rolesBusiness;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> FindAll()
        {
            try
            {
                var result = _userBusiness.Consulta.Where(p=>p.UsuarioExclusao == "nulo");
                if (result != null)
                {
                    return result;
                }

                return (IEnumerable<dynamic>)BadRequest("Não item encotrado");

            }
            catch (Exception)
            {

                throw;
            }
           

        }

        private IEnumerable<dynamic> OK(IQueryable<User> result)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Inserir([FromBody] UserVM modelVm)
        {

            if (ModelState.IsValid)
            {
                User model = new User()
                {
                    id = Guid.Empty,
                    Username = modelVm.Username,
                    Password = modelVm.Password,
                    PhoneNumber = modelVm.PhoneNumber,
                    email = modelVm.email

                };

                _userBusiness.Inserir(model);

                Guid idRole = Guid.Parse(modelVm.RoleId);
                Guid idUser = Guid.Empty;
                               

                IQueryable<User> lastIdUser = _userBusiness.Consulta.OrderByDescending(p => p.DataInclusao).Take(1);

                if (lastIdUser != null)
                {
                    foreach (var item in lastIdUser)
                    {
                        if (item != null)
                        {
                            idUser = item.id;
                        }
                    }
                }
                var tInter = new UserRoles();
                tInter.idUser = idUser;
                tInter.idRole = idRole;

                _userRolesBusiness.Inserir(tInter);

                return Ok(model);
            }

            return BadRequest("Cadastro não foi concluído!");

        }


        [HttpDelete("{id}")]
        //[Route("Excluir/id")]
        public async Task<ActionResult<dynamic>> Excluir(string id)
        {
            var ID = Guid.Parse(id); ;

            if (ModelState.IsValid)
            {
                User user = _userBusiness.Consulta.Where(p => p.id == ID).FirstOrDefault();

                UserRoles userRoles = _userRolesBusiness.Consulta.Where(p => p.idUser == user.id).FirstOrDefault();

                if (user == null)
                {
                    error = "Registro user não encontrado!";
                    return error;
                }

                _userBusiness.Excluir(user);

                if (userRoles == null)
                {
                    error = "Registro userRoles não encontrado!";
                    return error;
                }

                _userRolesBusiness.Excluir(userRoles);

                return Ok(user);
            }

            return BadRequest("Exclusão não foi concluída!");

        }

        [JsonProperty("error")]
        public string error { get; set; }


    }
}
