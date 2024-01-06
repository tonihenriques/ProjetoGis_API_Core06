using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Hunger_Map.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentosController : ControllerBase
    {
        protected IListAlimentosBusiness _listAlimentosBusiness;

        public AlimentosController(IListAlimentosBusiness listAlimentosBusiness)
        {
            _listAlimentosBusiness = listAlimentosBusiness;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> ListAlimentos() 
        {
            try
            {
                var resp = _listAlimentosBusiness.Consulta.ToList();
                return resp;
            }
            catch (Exception)
            {
                throw;            }
        }

        [HttpPost]
        public ActionResult<dynamic> Inserir([FromBody] AlimentosVM alimentoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ListAlimentos alimentos = new ListAlimentos();
                    alimentos.nome = alimentoVM.nome;
                    alimentos.medida = alimentoVM.medida;
                    _listAlimentosBusiness.Inserir(alimentos);
                    return Ok(alimentos);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        //[Route("Excluir/id")]
      
        public async Task<ActionResult<dynamic>> Excluir(string id)
        {

            try
            {

                var ID = Guid.Parse(id); ;

                if (ModelState.IsValid)
                {
                    ListAlimentos alimento = _listAlimentosBusiness.Consulta.Where(p => p.id == ID).FirstOrDefault();
                   

                    if (alimento == null)
                    {
                        error = "Registro não encontrado!";
                        return error;
                    }

                    _listAlimentosBusiness.Excluir(alimento);

                    return Ok(alimento);
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

