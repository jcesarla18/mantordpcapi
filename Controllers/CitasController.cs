using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    // [ApiController]
    [Route("api/[controller]")]
    public class CitasController(ICitasService citasService) : ControllerBase
    {
        private readonly ICitasService _citasService = citasService;
        [HttpGet]
        public async Task<IActionResult> GetCitasAsync(){
            try
            {
                var citas = await _citasService.GetCitasAsync();
                return Ok(citas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Id")]
        public async Task<ActionResult> FindCitasIdAsync(int Id){
            try
            {
                if(Id <= 0){
                    return NotFound();
                }
                var citas = await _citasService.FindCitasIdAsync(Id);
                return Ok(citas);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]     
        public async Task<ActionResult> SaveCitasAsync(Cita cita){
            try
            {
                if (cita == null){
                    return NotFound();
                }
                var rpta = await _citasService.CreateCitasAsync(cita);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCitasAsync(Cita cita){
            try
            {
                if(cita == null){
                    return NotFound();
                }
                var existingCita = await _citasService.FindCitasIdAsync(cita.CitaId);
                if(existingCita == null){
                    return BadRequest();
                }
                existingCita.ClienteId = cita.ClienteId<= 0?existingCita.ClienteId : cita.ClienteId;
                existingCita.OrdenadorId = cita.OrdenadorId<= 0?existingCita.OrdenadorId : cita.OrdenadorId;
                existingCita.TecnicoId = cita.TecnicoId<= 0?existingCita.TecnicoId : cita.TecnicoId;
                existingCita.ServicioId = cita.ServicioId<= 0?existingCita.ServicioId :cita.ServicioId;
                existingCita.FechaCita = cita.FechaCita != Convert.ToDateTime("") ?  existingCita.FechaCita: cita.FechaCita;
                existingCita.Estado = cita.Estado ?? existingCita.Estado;
                var rpta = await _citasService.UpdateCitasAsync(existingCita);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCita(int Id){
            try
            {
                if(Id <= 0){
                    return NotFound();
                }
                var existingCita = await _citasService.FindCitasIdAsync(Id);
                if(existingCita == null){
                    return NotFound();
                }
                var rpta = await _citasService.DeleteCitasAsync(existingCita);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
} 