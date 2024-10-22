using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Contract{
    public interface IClienteService{
        Task<List<Cliente>> GetClienteAsync();
        Task<Cliente> CreateClienteAsync(Cliente model);
        Task<Cliente> UpdateClienteAsync(Cliente model);
        Task<bool> DeleteClienteAsync(Cliente model);
        Task<Cliente> FindClienteAsync(Cliente model);
        Task<Cliente> FindClienteIdAsync(int Id);
    }

}