using DatingApp.API.Dtos;
using DatingApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            try
            {
                var createdUser = await _authService.Register(userForRegister);

                if (createdUser == null)
                    return BadRequest(new { sucesso = false, mensagem = "Usuário já existe" });

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
                var token = await _authService.Login(userForLogin);

                if (token == null)
                    return Unauthorized();

                return Ok(new
                {
                    sucesso = true,
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, erro = ex.Message });
            }
        }
    }
}