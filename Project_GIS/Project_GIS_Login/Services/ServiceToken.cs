using Microsoft.IdentityModel.Tokens;
using Project_GIS_Login.auth_settings;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_GIS_Login.Services
{
    public static class ServiceToken
    {
        public static string GenerateToken(UserVM user)
        {

            #region token01
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(settings.secret);
            //var tokendescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Username.ToString()),
            //        new Claim(ClaimTypes.Role, "Manager")
            //    }
            //    ),

            //    Expires = DateTime.UtcNow.AddMinutes(5),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //    SecurityAlgorithms.HmacSha256Signature
            //    )
            //};

            //var token = tokenHandler.CreateToken(tokendescriptor);

            //return tokenHandler.WriteToken(token);

            #endregion

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5130",
                audience: "http://localhost:5130",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials

                );

            var tokenstring = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenstring;

        }
    }
}
