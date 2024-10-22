using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Implement
{
    public class CitasService(DbmantordContext dbmantordContext) : ICitasService
    {
        private readonly DbmantordContext _dbmantorContext = dbmantordContext;

        public async Task<List<Cita>> GetCitasAsync()
        {
            List<Cita> citas = await _dbmantorContext.Citas.ToListAsync();
            return citas ?? new();
        }
       
        public async Task<Cita> FindCitasIdAsync(int Id)
        {
            Cita cita= await _dbmantorContext.Citas.FindAsync(Id)?? new();
            return cita;
        }
        public async Task<Cita> CreateCitasAsync(Cita cita)
        {
            _dbmantorContext.Citas.Add(cita);
            await _dbmantorContext.SaveChangesAsync();
            return cita;
        }
        public async Task<Cita> UpdateCitasAsync(Cita cita)
        {
            _dbmantorContext.Citas.Update(cita);
            await _dbmantorContext.SaveChangesAsync();
            return cita;
        }

         public async Task<bool> DeleteCitasAsync(Cita cita)
        {
            _dbmantorContext.Citas.Remove(cita);
            await _dbmantorContext.SaveChangesAsync();
            return true;
        }
    }
}