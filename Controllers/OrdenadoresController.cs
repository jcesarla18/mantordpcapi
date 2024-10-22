using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MantOrdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenadoresController(IOrdernadoresService ordernadoresService) : ControllerBase
    {
        private readonly IOrdernadoresService _ordernadoresService = ordernadoresService;

        [HttpGet]
        public async Task<IActionResult> GetOrdenadoresAsyn()
        {
            try
            {
                var ordenadores = await _ordernadoresService.GetOrdenadoresAsync();
                return Ok(ordenadores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> FindOrdenadoresIdAsync(int Id){
            try
            {   if(string.IsNullOrEmpty(Id.ToString())){
                return NotFound();
            }
                var ordenadores = await _ordernadoresService.FindOrdenadoresIdAsync(Id);
                return Ok(ordenadores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrdenadoresAsync(Ordenadore ordenadore){
            try
            {
                if(ordenadore == null){
                    return NotFound();
                }
                var ordenadores = await _ordernadoresService.CreateOrdenadoresAsync(ordenadore);
                return Ok(ordenadores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdenadoresAsync(int id ,Ordenadore ordenadore){
            try
            {
                if (string.IsNullOrEmpty(ordenadore.OrdenadorId.ToString()) ){
                    return NotFound();
                }
                var existingOrdenadores = await _ordernadoresService.FindOrdenadoresIdAsync(ordenadore.OrdenadorId);
                Ordenadore Oordenadores = new ();
                if(existingOrdenadores != null){
                    existingOrdenadores.Marca = ordenadore.Marca?? existingOrdenadores.Marca;
                    existingOrdenadores.Modelo = ordenadore.Modelo?? existingOrdenadores.Modelo;
                    existingOrdenadores.NumeroSerie = ordenadore.NumeroSerie?? existingOrdenadores.NumeroSerie;
                    existingOrdenadores.FechaCompra = ordenadore.FechaCompra?? existingOrdenadores.FechaCompra;
                    Oordenadores = await _ordernadoresService.UpdateOrdenadoresAsync(existingOrdenadores);
                } 
                return Ok(Oordenadores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrdenadoresAsync(int Id){
            try
            {
                if(Id <= 0){
                    return NotFound();
                };
                var ordenadore = await _ordernadoresService.FindOrdenadoresIdAsync(Id);
                if(ordenadore==null){
                    return NotFound();
                }
                var rpta = await _ordernadoresService.DeleteOrdenadoresAsync(ordenadore);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}