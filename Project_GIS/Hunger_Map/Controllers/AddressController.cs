using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.Utils;
using Hunger_Map.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace Hunger_Map.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {        
        
        protected IAddressBusiness _addressBusiness;       
        private readonly HttpClient _client;

        public AddressController(IAddressBusiness addressBusiness, HttpClient client)
        {
            _addressBusiness = addressBusiness;           
            _client = client;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Address>> Get()
        {
            var result = _addressBusiness.Consulta.ToList();
            return Ok(result);
        }

        [HttpGet("UserAddress")]
       // [Authorize]
        public async Task<IEnumerable<dynamic>> GetUserAddress()
        {
            try
            {
                var response = await _client.GetAsync("https://localhost:5130/api/User");
                var content = await response.Content.ReadAsStringAsync();

                var userlist = JsonConvert.DeserializeObject<IEnumerable<UserVM>>(content);

                var address = _addressBusiness.Consulta.ToList();

              
                List<AddressUserVM>  usersadVM = new List<AddressUserVM>();
                List<AddressUserVM>  usersatempdVM = new List<AddressUserVM>();

                foreach(var item in userlist)
                {
                    AddressUserVM useradVM = new AddressUserVM();
                    if (item != null )
                    {
                        useradVM.TotalPessoas = item.Totalpessoas;
                        useradVM.Menor10 = item.Menor10;
                        useradVM.Maior60 = item.Maior60;    
                        useradVM.Masculino = item.Masculino;
                        useradVM.Feminino = item.Feminino;
                        useradVM.email = item.email;
                        useradVM.role = item.Role;
                        usersadVM.Add(useradVM);
                    }
                }

                foreach(var item in address)
                {
                    AddressUserVM useradVM = new AddressUserVM();
                    if(item != null)
                    {
                        useradVM.rua = item.rua;
                        useradVM.numero = item.numero;
                        useradVM.bairro = item.bairro;
                        useradVM.cidade = item.cidade;
                        useradVM.estado = item.estado;
                        useradVM.cep = item.cep;
                        useradVM.pais = item.pais;
                        useradVM.email = item.email;
                        useradVM.lat = item.lat;
                        useradVM.lng = item.lng;
                        
                        usersatempdVM.Add(useradVM);
                    }

                }

                var sadVM = from u in usersadVM
                            join a in address
                            on u.email equals a.email
                            where u.email.Equals(a.email)
                            select new AddressUserVM()
                            {
                               
                                rua = a.rua,
                                numero = a.numero,
                                bairro = a.bairro,
                                cidade = a.cidade,
                                estado = a.estado,
                                pais = a.pais,
                                cep = a.cep,
                                lat = a.lat,
                                lng = a.lng,
                                email = a.email,
                                role = u.role,
                                username = u.username,
                                PhoneNumber = u.PhoneNumber,
                                TotalPessoas = u.TotalPessoas,
                                Menor10 = u.Menor10,
                                Maior60 = u.Maior60,
                                Feminino = u.Feminino,
                                Masculino = u.Masculino

                            };


                return sadVM;
            }
            catch (Exception)
            {

                throw;
            }

        }



        private async Task<IEnumerable<UserVM>> GetUser()
        {
            var response = await _client.GetAsync("https://localhost:5130/api/User");           

            return await response.ReadContentAs<List<UserVM>>();

        }



        [HttpGet("UserAnjo/{email}")]
        // [Authorize]
        public async Task<IEnumerable<dynamic>> GetUserAnjo(string email)
        {
            try
            {
                var response = await _client.GetAsync("https://localhost:5130/api/User");
                var content = await response.Content.ReadAsStringAsync();

                var userlist = JsonConvert.DeserializeObject<IEnumerable<UserVM>>(content);

                var address = _addressBusiness.Consulta.ToList();


                List<AddressUserVM> usersadVM = new List<AddressUserVM>();
                List<AddressUserVM> usersatempdVM = new List<AddressUserVM>();

                foreach (var item in userlist)
                {
                    AddressUserVM useradVM = new AddressUserVM();
                    if (item != null && item.Role == "Anjo" && item.email == email)
                    {
                        useradVM.TotalPessoas = item.Totalpessoas;
                        useradVM.Menor10 = item.Menor10;
                        useradVM.Maior60 = item.Maior60;
                        useradVM.Masculino = item.Masculino;
                        useradVM.Feminino = item.Feminino;
                        useradVM.email = item.email;
                        usersadVM.Add(useradVM);
                    }
                }

                foreach (var item in address)
                {
                    AddressUserVM useradVM = new AddressUserVM();
                    if (item != null && item.email == email)
                    {
                        useradVM.rua = item.rua;
                        useradVM.numero = item.numero;
                        useradVM.bairro = item.bairro;
                        useradVM.cidade = item.cidade;
                        useradVM.estado = item.estado;
                        useradVM.cep = item.cep;
                        useradVM.pais = item.pais;
                        useradVM.email = item.email;
                        useradVM.lat = item.lat;
                        useradVM.lng = item.lng;

                        usersatempdVM.Add(useradVM);
                    }

                }

                var sadVM = from u in usersadVM
                            join a in usersatempdVM
                            on u.email equals a.email
                            where u.email.Equals(a.email)
                            select new AddressUserVM()
                            {
                                role = a.role,
                                rua = a.rua,
                                numero = a.numero,
                                bairro = a.bairro,
                                cidade = a.cidade,
                                estado = a.estado,
                                pais = a.pais,
                                cep = a.cep,
                                lat = a.lat,
                                lng = a.lng,
                                email = a.email,
                                username = u.username,
                                PhoneNumber = u.PhoneNumber,
                                TotalPessoas = u.TotalPessoas,
                                Menor10 = u.Menor10,
                                Maior60 = u.Maior60,
                                Feminino = u.Feminino,
                                Masculino = u.Masculino

                            };


                return sadVM;
            }
            catch (Exception)
            {

                throw;
            }

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
