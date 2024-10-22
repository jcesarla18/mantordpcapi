using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(IClienteService clienteService) : Controller
    {
        public readonly IClienteService _clienteService = clienteService;
        
        [HttpGet]
        public async Task<IActionResult> GetClienteAsync(){
            try
            {
                var cliente = await _clienteService.GetClienteAsync();
                return Ok(cliente);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClienteIdAsync(int Id){
            try
            {
                var cliente = await _clienteService.FindClienteIdAsync(Id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveClienteAsync(Cliente modelo){
            try
            {
                var cliente = await _clienteService.CreateClienteAsync(modelo);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClienteAsync(Cliente modelo, int id){
            try
            {
                Cliente Ocliente = new();
                var exitingCliente = await _clienteService.FindClienteIdAsync(id);
                if (exitingCliente != null){
                    exitingCliente.Apellido = modelo.Apellido?? exitingCliente.Apellido;
                    exitingCliente.Telefono = modelo.Telefono?? exitingCliente.Telefono;
                    exitingCliente.Direccion = modelo.Direccion?? exitingCliente.Direccion;
                    Ocliente = await _clienteService.UpdateClienteAsync(exitingCliente);
                }
                return Ok(Ocliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> RemoveClienteAsync(int Id){
            try
            {
                var existinCliente = await _clienteService.FindClienteIdAsync(Id);
                if (existinCliente == null){
                    return NotFound();
                }
                bool rpta = await _clienteService.DeleteClienteAsync(existinCliente);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}