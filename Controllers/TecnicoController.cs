using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    // [ApiController]
    [Route("api/[controller]")]
    public class TecnicoController(ITecnicoService tecnicoService) : ControllerBase
    {
        private readonly ITecnicoService _tecnicoService = tecnicoService;
        [HttpGet]
        public async Task<IActionResult> GetTecnicoAsync(){
            try
            {
                var tecnico =  await _tecnicoService.GetTecnicoAsync();
                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Id")]
        public async Task<ActionResult> FindTecnicoId(int Id){
            try
            {
                if (Id <= 0){
                    return BadRequest();
                }
                var tecnico = await _tecnicoService.FindTecnicoIdAsync(Id);
                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]     
        public async Task<ActionResult> SaveOrdenadoresAsync(Tecnico tecnico){
            try
            {
                if (tecnico == null){
                    return NotFound();
                }
                var rpta = await _tecnicoService.CreateTecnicoAsync(tecnico);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateTecnicoAsync(Tecnico tecnico){
            try
            {
                if(tecnico == null){
                    return NotFound();
                }
                var existingTecnico = await _tecnicoService.FindTecnicoIdAsync(tecnico.TecnicoId);
                if(existingTecnico == null){
                    return BadRequest();
                }
                existingTecnico.Nombre = tecnico.Nombre ?? existingTecnico.Nombre;
                existingTecnico.Apellido = tecnico.Apellido ?? existingTecnico.Apellido;
                existingTecnico.Email = tecnico.Email ?? existingTecnico.Email;
                existingTecnico.Telefono = tecnico.Telefono ?? existingTecnico.Telefono;
                existingTecnico.Especialidad = tecnico.Especialidad ?? existingTecnico.Especialidad;
                var rpta = await _tecnicoService.UpdateTecnicoAsync(existingTecnico);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTecnicoAsync(int Id){
            try
            {
                if(Id <= 0){
                    return NotFound();
                }
                var existingTecnico = await _tecnicoService.FindTecnicoIdAsync(Id);
                if(existingTecnico == null){
                    return NotFound();
                }
                var rpta = await _tecnicoService.DeleteTecnicoAsync(existingTecnico);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}