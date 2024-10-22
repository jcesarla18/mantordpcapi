
using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Contract
{
    public interface ICitasService
    {
        Task<List<Cita>> GetCitasAsync();
        Task<Cita> CreateCitasAsync(Cita cita);
        Task<Cita> UpdateCitasAsync(Cita cita);
        Task<bool> DeleteCitasAsync(Cita cita);
        Task<Cita> FindCitasIdAsync(int Id);
    }
}