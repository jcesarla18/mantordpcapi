using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantOrdAPI.Models;
using MantOrdAPI.Services.Implement;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    // [ApiController]
    [Route("api/[controller]")]
    public class ServicioController(IServicioService servicioService) : ControllerBase
    {
        private readonly IServicioService _servicioService = servicioService;
        [HttpGet]
        public async Task<IActionResult> GetServiciosAsync(){
            try
            {
                var servicios =  await _servicioService.GetServicioAsync();
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Id")]
        public async Task<ActionResult> FindServiciosId(int Id){
            try
            {
                if (Id <= 0){
                    return BadRequest();
                }
                var servicios = await _servicioService.FindServicioIdAsync(Id);
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]     
        public async Task<ActionResult> SaveServiceAsync(Servicio servicio){
            try
            {
                if (servicio == null){
                    return NotFound();
                }
                var rpta = await _servicioService.CreateServicioAsync(servicio);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateServicioAsync(Servicio servicio){
            try
            {
                if(servicio == null){
                    return NotFound();
                }
                var existingServicio = await _servicioService.FindServicioIdAsync(servicio.ServicioId);
                if(existingServicio == null){
                    return BadRequest();
                }
                existingServicio.Nombre = servicio.Nombre ?? existingServicio.Nombre;
                existingServicio.Descripcion = servicio.Descripcion ?? existingServicio.Descripcion;
                existingServicio.Precio= servicio.Precio <=0? existingServicio.Precio : servicio.Precio;
                existingServicio.DuracionMinutos = servicio.DuracionMinutos <= 0? existingServicio.DuracionMinutos: servicio.DuracionMinutos;

                var rpta = await _servicioService.UpdateServicioAsync(existingServicio);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteServicioAsync(int Id){
            try
            {
                if(Id <= 0){
                    return NotFound();
                }
                var existingServicio = await _servicioService.FindServicioIdAsync(Id);
                if(existingServicio == null){
                    return NotFound();
                }
                var rpta = await _servicioService.DeleteServicioAsync(existingServicio);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}