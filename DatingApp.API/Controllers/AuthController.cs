using DatingApp.API.Dtos;
using DatingApp.API.Models.UserAgg;
using DatingApp.API.Models.UserAgg.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            try
            {
                userForRegister.Username = userForRegister.Username.ToLower();

                if (await _repository.UserExists(userForRegister.Username))
                    return BadRequest(new { sucesso = false, mensagem = "Usuário já existe" });

                var userToCreate = new User
                {
                    Username = userForRegister.Username
                };

                var createdUser = await _repository.Register(userToCreate, userForRegister.Password);

                return Created(createdUser.Id, 
                    new {
                        sucesso = true,
                        mensagem = "Operação realizada com sucesso"
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, erro = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            try
            {
                var userFromRepo = await _repository.Login(userForLogin.Username.ToLower(), userForLogin.Password);

                if (userFromRepo == null)
                    return Unauthorized();

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id),
                    new Claim(ClaimTypes.Name, userFromRepo.Username),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    sucesso = true,
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, erro = ex.Message });
            }
        }
    }
}