using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SIMRIFA.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost()]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{

			if (loginModel.Username == "test" && loginModel.Password == "test")
			{
				var token = GenerateJwtToken(loginModel.Username);
				return Ok(new { token });
			}

			return Unauthorized();
		}

		private string GenerateJwtToken(string username)
		{
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Asegúrate de definir la clave en appsettings.json
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, username)
				}),
				Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:ExpireHours"])),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}


}
