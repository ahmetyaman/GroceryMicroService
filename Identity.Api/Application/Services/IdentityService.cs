using Identity.Api.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Api.Application.Services
{
    public class IdentityService : IIdentityService
    {

        public Task<LoginResponseModel> Login(LoginRequestModel requestModel)
        {

            //DYE sor ve  doğrula  sonra yapılacak iştir  karıştırmayalım sonrasında düzenleyelim


            var claims = new Claim[]
            {
                  new Claim(ClaimTypes.NameIdentifier,requestModel.UserName),
                  new Claim (ClaimTypes.Name,"AhmetYaman")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GorceryProjectSaltBeltTokenKeyOyeah"));


            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: credentials, notBefore: DateTime.Now);


            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(token);

            LoginResponseModel response = new()
            {

                UserToken = encodedJWT,
                UserName=requestModel.UserName
            };


            return Task.FromResult(response);
        }
    }
}
