using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Business.Concrect;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.ViewModels;
using Project_GIS_Login.Services;
using Microsoft.AspNetCore.Authorization;

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
       // [Authorize]
        public async Task<IEnumerable<UserVM>> FindAll()
        {
           
                var result = _userBusiness.Consulta.Where(p => p.UsuarioExclusao == "nulo");                  
                        
                        if (result != null)
                        {
                            List<UserVM> usersVM = new List<UserVM>();
                           

                            foreach (var item in result)
                            {
                                UserVM userVM = new UserVM();
                                if (item != null)
                                {
                                    userVM.Username = item.Username;
                                    userVM.Password = "";
                                    userVM.Role = "";
                                    userVM.PhoneNumber = item.PhoneNumber;
                                    userVM.RoleId = "";
                                    userVM.email = item.email;
                                    userVM.Totalpessoas = item.Totalpessoas;
                                    userVM.Menor10 = item.Menor10;
                                    userVM.Maior60 = item.Maior60;
                                    userVM.Feminino = item.Feminino;
                                    userVM.Masculino = item.Masculino;
                                    usersVM.Add(userVM);

                                }
                            }
                               
                                return usersVM;
                        }

            return null;
        }
              
        

        private IEnumerable<dynamic> OK(IQueryable<User> result)
        {
            throw new NotImplementedException();
        }

        [HttpPost]      
        public async Task<ActionResult<User>> Inserir([FromBody] UserVM modelVm)
        {
            try
            {

                var senha = Encrypt.CreateHashFromPassword(modelVm.Password);

                if (ModelState.IsValid)
                {
                    User model = new User()
                    {
                        id = Guid.Empty,
                        Username = modelVm.Username,
                        Password = senha,
                        PhoneNumber = modelVm.PhoneNumber,
                        email = modelVm.email,
                        Totalpessoas = modelVm.Totalpessoas,
                        Menor10 = modelVm.Menor10,
                        Maior60 = modelVm.Maior60,
                        Feminino = modelVm.Feminino,
                        Masculino = modelVm.Masculino,

                    };

                    _userBusiness.Inserir(model);

                    Role role = _rolesBusiness.Consulta.FirstOrDefault(p => p.Name.Equals("customer"));

                    Guid idRole = Guid.Empty;

                    if (modelVm.RoleId == "")
                    {
                        idRole = role.id;
                    }
                    else
                    {
                        idRole = Guid.Parse(modelVm.RoleId);
                    }


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
            catch (Exception)
            {

                throw;
            }

        }


        [HttpDelete("{id}")]
        //[Route("Excluir/id")]
        [Authorize]
        public async Task<ActionResult<dynamic>> Excluir(string id)
        {

            try
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
            catch (Exception)
            {

                throw;
            }

        }

        [JsonProperty("error")]
        public string error { get; set; }


    }
}
