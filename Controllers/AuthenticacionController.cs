using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MantOrdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticacionController(IUsuarioService usuarioService, IConfiguration configuration) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login model)
        {
            try
            {
                if(model.Email == "" && model.Password == "" ){
                    return NotFound();
                }  

                var usuario = await _usuarioService.Login(model.Email?? "");
                bool confirmarPass = BCrypt.Net.BCrypt.EnhancedVerify(model.Password, usuario.Contrasena);
                if(confirmarPass == false){
                    return Unauthorized(new {message = "Credenciales incorrectas"});
                }

                var token = GenerateJwtToken(usuario);
                return Ok(new{token});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // generar token
        private string GenerateJwtToken(Usuario user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["key"] ?? "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Rol)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationMinutes"]?? "")),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult> ValidatePassword([FromBody] Usuario model, int id){
            try                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            {
                 var usuario = await _usuarioService.FindUserIdAsync(model.UsuarioId);
                bool confirmarPass = BCrypt.Net.BCrypt.EnhancedVerify(model.Contrasena, usuario.Contrasena);
                if(confirmarPass == false){
                 return Unauthorized();
                }
                return Ok(confirmarPass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}