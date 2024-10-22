
using MantOrdAPI.Models;

namespace MantOrdAPI.Services.Contract
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario> CreateUserAsync(Usuario model, Cliente cliente);
        Task<Usuario> UpdateUserAsync(Usuario model, Cliente cliente);
        Task<bool> DeleteUserAsync(Usuario model, Cliente cliente);
        Task<Usuario> FindUserAsync(Usuario model);
        Task<Usuario> FindUserIdAsync(int Id);
        Task<Usuario> Login(string user);
        Task<bool> SignOff(string user, string password);

    }
}
