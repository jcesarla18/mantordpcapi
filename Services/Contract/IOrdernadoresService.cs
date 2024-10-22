
using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Contract
{
    public interface IOrdernadoresService
    {
        Task<List<Ordenadore>> GetOrdenadoresAsync();
        Task<Ordenadore> CreateOrdenadoresAsync(Ordenadore model);
        Task<Ordenadore> UpdateOrdenadoresAsync(Ordenadore model);
        Task<bool> DeleteOrdenadoresAsync(Ordenadore model);
        Task<Ordenadore> FindOrdenadoresIdAsync(int Id);
    }
}