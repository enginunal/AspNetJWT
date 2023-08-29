# AspNetJWT - Asymmetric - PS256


> Base WebApi sample Project is on the webapi-base branch: https://github.com/enginunal/AspNetJWT/tree/webapi-base

</br>


Packages:
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.10
```

appsettings.json->
```
  "JwtTokenOptions": {
    "Issuer": "AspNetJWT",
    "Audience": "ItemsAud",
    "Expiration": 10,
    "PrivateKey": "MIIEpAIBAAKCAQEA42x8DXg7KuqaIiQVAktrUpSBfnHfNpZBZrVqhk676oF0V9Jo4R9S/ML+t6W6/3jLfYriwmJAUK6i5XihKijYcGsgqbDnk7ymD9sX6JQi+7YviQdNiPPHlbd6ULxwaFRp8VRoeHkJwz7cb1S46I4KXjSDLzg7V9UoILllvfwkw2g5HmukQCwirOPTfJU4X6W0F8Cc6KYWILGMWoL3L965JxXrSBLUAynMw2XiuudWbAirS5H6aVM58QP5bhO884azLCsgTmQr78EHEtY3KuopMR3h607n8UB0SsaBXc+suN/FMFStLlIktej1KpZjPOQYplFVb1Za4qm550W3+vA3uQIDAQABAoIBAAe/tYZb92Ah+Dh7lD+sxC5fIv5k8N8SRY4zVjrXSe3WlVk8sRgikhhMqJMnUXTM79oKNmm7IUfee2xoLM2b8Kv76nP9tBZDkkDuDSV+jqaW8Y2wswKck8tVIhTIJuhXH/j6EEkyjpOZe7dLYrWByXamQWYFe3glqiVbS80qXzJNNVehJZKWE4C0p95zylLeve87z6ESRqEFEGGajg1fJ1y8HiBFLLIeo8PhXVQb+12Lli4nt4J5e26vGftZbEBc2hgHmy+z03lUIwlLjHtnSIMncdVNoDxpIGrk000tan77RMbUtEtoJDrDlT31nUihxghDKCo40M6dWIAn/mYF5QUCgYEA/BLInz+lgE3XLxVjDoTl5oln4d1iLfzmAx/D+AKeIsg+t4wEVFhJ8z35t9+Rgge2+0vdFIXt0cmCxT6xgvDwPB4tzRgihowierGs/LBA3ALChw87Km7ZhmZPNi0N1ZKOOQUpIEUVqquMRBuIPjGhX+/zrV9HD1EYgrhxllxnLbMCgYEA5vdnRFDufdMqYj274GgZBLPdRlmT9m+5fbBDczRzhRJuxwH0enmJ/pswpKtDi/233eLUAUQoFT9ncn/umLsJvpsmknXEjJu5j/+lhy5zMv37Zj0DZiV/tkmrNH2Z3fuVVHC0MRGKee/JQkAjq3498epkfHRgHallSzAlzxLdhuMCgYEAyffK4z29v2WfCkyah6GYCvUBVQlqhupg6RPkkqyWQ8wp5Cq9tU7gQZLgqKDrF0JTLFoGk1ET0ckhjCTFWeLuuVx82h2CXEefwKrnrbcS4LUdY9WiVEdo5rTGtuO2d+7rIWivIPn5eDvnMRLWQO7HG/VISxtR9kXF28coy47R5N0CgYB5IXmsT5G0tcG9wki9WXr9h2NeWO6LyW6L55yIWBLZ0J/+iRzx+roPqM5rHlMPoWID4cl0XI11D0qutz+oJDfpGi+xkkghwi4gwl/KtOa/8IofxZH5yWLgdGRw5AqFbEOWBIHMVjm817BCqabLcysDq/1FdDdQdx3jv/2kS22ZJwKBgQD6ZZc5imJ3R78wJt/PXkYXlqF/pvN83ImwSTGhyyDi+sLdhwqXhxGOoD9MlhR2Cv5bufzxYzsrRvpLt4szu6lE/9b+MjIjCjQqkGVxOcQjlTGuQg8Xd/pkhNePghD0aFKvzVThsNclhntiGkRvroG7eGW19gCWa71o54RzF6I5LQ==",
    "PublicKey": "MIIBCgKCAQEA42x8DXg7KuqaIiQVAktrUpSBfnHfNpZBZrVqhk676oF0V9Jo4R9S/ML+t6W6/3jLfYriwmJAUK6i5XihKijYcGsgqbDnk7ymD9sX6JQi+7YviQdNiPPHlbd6ULxwaFRp8VRoeHkJwz7cb1S46I4KXjSDLzg7V9UoILllvfwkw2g5HmukQCwirOPTfJU4X6W0F8Cc6KYWILGMWoL3L965JxXrSBLUAynMw2XiuudWbAirS5H6aVM58QP5bhO884azLCsgTmQr78EHEtY3KuopMR3h607n8UB0SsaBXc+suN/FMFStLlIktej1KpZjPOQYplFVb1Za4qm550W3+vA3uQIDAQAB"
  }
```

Program.cs->
```
var rsa = RSA.Create();
rsa.ImportRSAPublicKey(Convert.FromBase64String(builder.Configuration["JwtTokenOptions:PublicKey"]), out _);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtTokenOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtTokenOptions:Audience"],
            IssuerSigningKey = new RsaSecurityKey(rsa)
        };
    });
```

Adding token controller 
```
dotnet aspnet-codegenerator controller -name TokenController -async -api -outDir Controllers
```

TokenController.cs->
```
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
```

Program.cs->
```
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetJWT", Version = "1.0" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
```

ItemsController.cs->
```
[Authorize]
```

### Swagger Screen
![Services](SwaggerOutput_HMAC256.jpg)











