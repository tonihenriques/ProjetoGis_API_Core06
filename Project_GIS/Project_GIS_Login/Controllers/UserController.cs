using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Entidade;

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

            var result = _userBusiness.Consulta;
            if (result != null)
            {
                return result;
            }


            return (IEnumerable<dynamic>)BadRequest("Não item encotrado");

        }

        private IEnumerable<dynamic> OK(IQueryable<User> result)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Inserir([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                _userBusiness.Inserir(model);

                Guid idUser = Guid.Empty;
                Guid idRole = Guid.Empty;

                var roles = _rolesBusiness.Consulta.FirstOrDefault(p => p.Name == "Admin");

                if (roles != null)
                {
                    idRole = roles.id;
                }

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


    }
}
