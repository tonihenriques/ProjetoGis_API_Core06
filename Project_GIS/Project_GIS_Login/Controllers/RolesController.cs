using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Business.Concrect;
using Project_GIS_Login.Context;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;
using Project_GIS_Login.ViewModels;
using System.Runtime.CompilerServices;

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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<dynamic>> Inserir([FromBody] RoleVM roleVM)
        {
            Role role = new Role()
            { 
                Name = roleVM.Name,
            
            };



            if (ModelState.IsValid)
            {
                Role srole = _roleBusiness.Consulta.Where(p => p.Name == role.Name).FirstOrDefault();

                if (srole != null)
                {
                    error = "Já existe um cadastro com esse nome!";
                    return error;
                }

                _roleBusiness.Inserir(role);

                return Ok(role);
            }

            return BadRequest("O Cadastro não foi concluído!");

        }


        [HttpDelete("{id}")]
        //[Route("Excluir/id")]
        [Authorize]
        public async Task<ActionResult<dynamic>> Excluir(string id)
        {
            var ID = Guid.Parse(id); ;

            if (ModelState.IsValid)
            {
                Role role = _roleBusiness.Consulta.Where(p => p.id == ID).FirstOrDefault();

                if (role == null)
                {
                    error = "Registro não encontrado!";
                    return error;
                }

                _roleBusiness.Excluir(role);

                return Ok(role);
            }

            return BadRequest("Exclusão não foi concluída!");

        }


        [JsonProperty("error")]
        public string error { get; set; }





    }




}
