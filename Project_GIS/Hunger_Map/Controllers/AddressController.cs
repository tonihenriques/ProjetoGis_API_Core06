using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hunger_Map.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {        
        
        protected IAddressBusiness _addressBusiness;
       
        public AddressController(IAddressBusiness addressBusiness)
        {           
            _addressBusiness = addressBusiness;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Address>> Get()
        {
            var result = _addressBusiness.Consulta.ToList();

            return Ok(result);

        }

        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> Inserir([FromBody] AddressVM addressVM)
        {

            Address address = new Address();
            address.rua = addressVM.rua;
            address.numero = addressVM.numero;
            address.bairro = addressVM.bairro;
            address.cidade = addressVM.cidade;
            address.estado = addressVM.estado;
            address.pais = addressVM.pais;
            address.cep = addressVM.cep;
            address.email = addressVM.email;
            address.lat = addressVM.lat;
            address.lng = addressVM.lng;


        _addressBusiness.Inserir(address);

            return Ok(address);

        }

    }
}
