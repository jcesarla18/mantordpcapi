using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Contract
{
    public interface ITecnicoService
    {
        Task<List<Tecnico>> GetTecnicoAsync();
        Task<Tecnico> CreateTecnicoAsync(Tecnico tecnico);
        Task<Tecnico> UpdateTecnicoAsync(Tecnico tecnico);
        Task<bool> DeleteTecnicoAsync(Tecnico tecnico);
        Task<Tecnico> FindTecnicoIdAsync(int Id);
    }
}