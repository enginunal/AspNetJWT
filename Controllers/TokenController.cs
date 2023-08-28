using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspNetJWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{pwd}")]
        public async Task<IActionResult> GetToken(string pwd)
        {
            if (pwd != "123") return Unauthorized("Şifre geçersiz.");
            string token = GenerateToken();
            return Ok(token);
        }

        private string GenerateToken()
        {
            var keyBytes = Encoding.UTF8.GetBytes(_config.GetSection("JwtTokenOptions")["SigningKey"]);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("sub", "eunal"),
                new Claim("name", "engin unal")
            };

            var roleClaims = new List<Claim>()
            {
                new Claim("role", "readers"),
                new Claim("role", "writers"),
            };

            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("JwtTokenOptions")["Issuer"],
                audience: _config.GetSection("JwtTokenOptions")["Audience"],
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromSeconds(double.Parse(_config.GetSection("JwtTokenOptions")["Expiration"]))),
                signingCredentials: signingCredentials);

            var tokenData = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenData;
        }

    }
}
