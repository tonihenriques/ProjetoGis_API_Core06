using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Context;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;

namespace Project_GIS_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        protected IBaseRepository<User> _baseRepository;
        protected LoginContext _loginContext;
        protected IRolesBusiness _roleBusiness;

        public RolesController(IBaseRepository<User> baseRepository, LoginContext loginContext, IRolesBusiness roleBusiness)
        {
            _baseRepository = baseRepository;
            _loginContext = loginContext;
            _roleBusiness = roleBusiness;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> FindAll()
        {

            var result = _roleBusiness.Consulta.ToList();

            if (result != null)
            {
                return result;
            }

            return result;

        }

        [HttpPost]
        [Route("Cadastro")]
        public async Task<ActionResult<Role>> Inserir([FromBody] Role model)
        {

            if (ModelState.IsValid)
            {
                _roleBusiness.Inserir(model);

                return Ok(model);
            }

            return BadRequest("Cadastro não foi concluído!");

        }
    }
}
