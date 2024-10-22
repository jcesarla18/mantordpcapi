using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUsuarioService usuarioService, IClienteService clienteService) : Controller
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IClienteService _clienteService = clienteService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsers()
        {
            List<Usuario> listaUsuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(listaUsuarios);
        }
        [HttpPost]
        public async Task<ActionResult> SaveUserAsync(Usuario modelo)
        {
            try
            {
                Usuario Ousuario = new();
                if (modelo != null)
                {

                    Cliente cliente = new()
                    {
                        UsuarioId = modelo.UsuarioId,
                        Nombre = modelo.Nombre,
                        Email = modelo.Email,
                    };
                    modelo.Contrasena = BCrypt.Net.BCrypt.EnhancedHashPassword(modelo.Contrasena, 10);
                    var usuario = await _usuarioService.CreateUserAsync(modelo, cliente);

                    Ousuario = new()
                    {
                        UsuarioId = usuario.UsuarioId,
                        Nombre = usuario.Nombre,
                        Email = usuario.Email,
                        Rol = usuario.Rol
                    };
                }
                return Ok(Ousuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> FindUserId(int Id)
        {
            try
            {
                Usuario usuario = await _usuarioService.FindUserIdAsync(Id);
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(Usuario modelo, int id)
        {
            try
            {
                if (id != modelo.UsuarioId)
                {
                    return BadRequest();
                }
                Usuario Ousuario = new();

                var existingUser = await _usuarioService.FindUserIdAsync(id);
                if (existingUser != null)
                {
                    existingUser.Nombre = modelo.Nombre==""? existingUser.Nombre:modelo.Nombre;
                    existingUser.Email = modelo.Email==""?  existingUser.Email: modelo.Email;
                    existingUser.Contrasena = modelo.Contrasena == ""? existingUser.Contrasena : BCrypt.Net.BCrypt.EnhancedHashPassword(modelo.Contrasena, 10);
                    existingUser.Rol = modelo.Rol== ""? existingUser.Rol :  modelo.Rol;

                    var existingCliente = await _clienteService.FindClienteIdAsync(Convert.ToInt32(existingUser.Clientes.FirstOrDefault()?.ClienteId));
                    existingCliente.Nombre = modelo.Nombre==""? existingUser.Nombre:modelo.Nombre;
                    existingCliente.Email = modelo.Email==""?  existingUser.Email: modelo.Email;

                    var usuario = await _usuarioService.UpdateUserAsync(existingUser, existingCliente);

                    Ousuario = new()
                    {
                        UsuarioId = usuario.UsuarioId,
                        Nombre = usuario.Nombre,
                        Email = usuario.Email,
                        Rol = usuario.Rol
                    };
                }
                else
                {
                    return NotFound();
                }
                return Ok(Ousuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            try
            {
                var existingUser = await _usuarioService.FindUserIdAsync(id);
                var existingCliente = await _clienteService.FindClienteIdAsync(Convert.ToInt32(existingUser.Clientes.FirstOrDefault()?.ClienteId));
                if (existingUser != null && existingCliente != null)
                {
                    bool isdelete = await _usuarioService.DeleteUserAsync(existingUser, existingCliente);
                }
                else
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}