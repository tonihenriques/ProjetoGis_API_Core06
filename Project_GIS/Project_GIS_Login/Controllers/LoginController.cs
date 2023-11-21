using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Context;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;
using Project_GIS_Login.ViewModels;
using static Project_GIS_Login.Services.ServiceToken;

namespace Project_GIS_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected IBaseRepository<User> _baseRepository;
        protected LoginContext _loginContext;
        protected ILoginBusiness _loginBusiness;
        public LoginController(IBaseRepository<User> baseRepository, LoginContext loginContext, ILoginBusiness loginBusiness)
        {
            _baseRepository = baseRepository;
            _loginContext = loginContext;
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {

            var user = new UserVM();

            var users = from u in _loginContext.Users
                        join c in _loginContext.UserRoless
                        on u.id equals c.idUser
                        join r in _loginContext.Roles
                                    on c.idRole equals r.id
                        where u.Username == model.Username && u.Password == model.Password
                        select new UserVM()
                        {
                            Username = u.Username,
                            Role = r.Name

                        };

            if (users != null)
            {
                foreach (var item in users)
                {
                    if (item != null)
                    {
                        user.Username = item.Username;
                        user.Password = item.Password;
                        user.Role = item.Role;
                    }
                }
            }

            //var user = UserRepository.Get(model.Username, model.Password);  




            if (user == null)
            {

                return NotFound(new { message = "Usuario ou Senha inválidos!" });
            }
            else
            {
                #region token01
                //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.secret));
                //var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //var claims = new List<Claim>()
                //{
                //    new Claim(ClaimTypes.Name, user.Username),
                //    new Claim(ClaimTypes.Role, "Manager")

                //};

                //var tokenOptions = new JwtSecurityToken(
                //    issuer: "http://localhost:5130",
                //    audience: "http://localhost:5130",
                //    claims: claims,
                //    expires: DateTime.Now.AddMinutes(5),
                //    signingCredentials: signingCredentials

                //    );


                //var tokenstring = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                //return Ok(new { Token = tokenstring });

                #endregion

                var token = GenerateToken(user);

                return Ok(new { Token = token });


            }


            //return Unauthorized();
            //var token = JsonSerializer.Serialize( ServiceToken.GenerateToken(user));

            //user.Password = "";

            //return token;

        }


    }
}
