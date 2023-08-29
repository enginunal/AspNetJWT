using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
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
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(_config["JwtTokenOptions:PrivateKey"]), out _);

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

            var handler = new JsonWebTokenHandler();
            var now = DateTime.UtcNow;
            var tokenData = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _config["JwtTokenOptions:Issuer"],
                Audience = _config["JwtTokenOptions:Audience"],
                IssuedAt = now,
                NotBefore = now,
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddMinutes(double.Parse(_config["JwtTokenOptions:Expiration"])),
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSsaPssSha256)
            });

            return tokenData;
        }
        
    }
}
