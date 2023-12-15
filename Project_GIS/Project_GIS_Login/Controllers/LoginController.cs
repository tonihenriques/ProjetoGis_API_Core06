using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Business.Concrect;
using Project_GIS_Login.Context;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;
using Project_GIS_Login.Services;
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
        protected IRolesBusiness _roleBusiness;
        protected IUserRolesBusiness _userroleBusiness;

        public LoginController(IBaseRepository<User> baseRepository, LoginContext loginContext, ILoginBusiness loginBusiness, 
            IRolesBusiness roleBusiness, IUserRolesBusiness userroleBusiness)
        {
            _baseRepository = baseRepository;
            _loginContext = loginContext;
            _loginBusiness = loginBusiness;
            _roleBusiness = roleBusiness;
            _userroleBusiness = userroleBusiness;
        }

        [HttpPost]
        //[Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserVM userVm)
        {

            var user = new UserVM();

            var senha = Encrypt.CreateHashFromPassword(userVm.Password);

            var user1 = _loginBusiness.Consulta.Where(p=>p.Username == userVm.Username && p.Password == senha).FirstOrDefault();
          
            if(user1 != null)
            {
                var role1 = _userroleBusiness.Consulta.Where(p => p.idUser == user1.id).FirstOrDefault();

                if(role1 != null)
                {
                    var role2 = _roleBusiness.Consulta.Where(p => p.id == role1.idRole).FirstOrDefault();

                    user.Username = user1.Username;
                    user.Password = user1.Password;
                    user.Role = role2.Name;
                    user.email = user1.email;
                }

            }

            if (user1 == null)
            {
                return "Usuario ou Senha inválidos!";
            }
            
                TokenVM tokenVM = new TokenVM()
                {
                    token = GenerateToken(user),
                    role = user.Role,
                    email = user.email
                };
           
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

                //var token = GenerateToken(user);

                return Ok(new { Token = tokenVM });


            }


            //return Unauthorized();
            //var token = JsonSerializer.Serialize( ServiceToken.GenerateToken(user));

            //user.Password = "";

            //return token;

        }


    
}
