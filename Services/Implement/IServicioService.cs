using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Implement
{
    public interface IServicioService
    {
        Task<List<Servicio>> GetServicioAsync();
        Task<Servicio> CreateServicioAsync(Servicio servicio);
        Task<Servicio> UpdateServicioAsync(Servicio servicio);
        Task<bool> DeleteServicioAsync(Servicio servicio);
        Task<Servicio> FindServicioIdAsync(int Id);
    }
}