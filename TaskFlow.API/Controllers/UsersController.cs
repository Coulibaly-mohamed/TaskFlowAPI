using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services.Interfaces;
using TaskFlow.API.Helpers;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        // POST: /api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            // Vérifie si l'email existe déjà
            if (await _userService.EmailExistsAsync(dto.Email))
                return BadRequest("Email déjà utilisé.");

            // Inscription de l'utilisateur
            var user = await _userService.RegisterAsync(dto);

            // Retourne l'ID et l'email de l'utilisateur
            return Ok(new { user.Id, user.Email });
        }
    


    // POST: /api/users/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            // Appel de la méthode LoginAsync qui retourne un token JWT (string)
            var token = await _userService.LoginAsync(dto);

            // Si le token est null ou vide, c'est que l'authentification a échoué
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Email ou mot de passe invalide.");

            // Retourne le token JWT en réponse
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            // Retourne une erreur 401 en cas de mauvais identifiants
            return Unauthorized(ex.Message); 
        }
        catch (Exception ex)
        {
            // Retourne une erreur 500 si une autre exception se produit
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

  }
}
