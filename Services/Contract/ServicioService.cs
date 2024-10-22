using MantOrdAPI.Models;
using MantOrdAPI.Services.Implement;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Contract
{
    public class ServicioService(DbmantordContext dbmantordContext) : IServicioService
    {
        private readonly DbmantordContext _dbmantordContext = dbmantordContext;

        public async Task<List<Servicio>> GetServicioAsync()
        {
            List<Servicio> servicioList = await _dbmantordContext.Servicios.ToListAsync() ?? new();
            return servicioList;
        }
        public async Task<Servicio> FindServicioIdAsync(int Id)
        {
            Servicio servicio = await _dbmantordContext.Servicios.FindAsync(Id) ?? new();
            return servicio;
        }

        public async Task<Servicio> CreateServicioAsync(Servicio servicio)
        {
            _dbmantordContext.Servicios.Add(servicio);
            await _dbmantordContext.SaveChangesAsync();
            return servicio;
        }
        public async Task<Servicio> UpdateServicioAsync(Servicio servicio)
        {
            _dbmantordContext.Servicios.Update(servicio);
            await _dbmantordContext.SaveChangesAsync();
            return servicio;
        }

        public async Task<bool> DeleteServicioAsync(Servicio servicio)
        {
            _dbmantordContext.Servicios.Remove(servicio);
            await _dbmantordContext.SaveChangesAsync();
            return true;
        }

    }
}