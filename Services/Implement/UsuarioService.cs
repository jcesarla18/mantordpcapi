using System.Transactions;
using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Implement
{
    public class UsuarioService(DbmantordContext dbmantordContext ) : IUsuarioService
    {
        private readonly DbmantordContext _dbmantordContext = dbmantordContext;
        public async Task<Usuario> CreateUserAsync(Usuario model, Cliente cliente)
        {
            try
            {
                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {

                    //insert users
                    _dbmantordContext.Usuarios.Add(model);
                    await _dbmantordContext.SaveChangesAsync();

                    //insert client 
                    cliente.UsuarioId = model.UsuarioId;
                    _dbmantordContext.Clientes.Add(cliente);
                    await _dbmantordContext.SaveChangesAsync();
                    //insert auditoria client
                    scope.Complete();
                }

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Usuario> UpdateUserAsync(Usuario model, Cliente cliente)
        {
            try
            {
                 using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //update users
                    _dbmantordContext.Usuarios.Update(model);
                    await _dbmantordContext.SaveChangesAsync();
                    //update client
                    _dbmantordContext.Clientes.Update(cliente);
                    await _dbmantordContext.SaveChangesAsync();
                    scope.Complete();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUserAsync(Usuario model, Cliente cliente)
        {
            try
            {
                bool rpta = false;
                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _dbmantordContext.Clientes.Remove(cliente);
                    await _dbmantordContext.SaveChangesAsync();
                    
                    _dbmantordContext.Usuarios.Remove(model);
                    await _dbmantordContext.SaveChangesAsync();

                    scope.Complete();
                    rpta = true;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Usuario> FindUserAsync(Usuario model)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> FindUserIdAsync(int Id)
        {
            try
            {
                Usuario? usuario = await _dbmantordContext.Usuarios.Include("Clientes").Where(u => u.UsuarioId == Id).FirstOrDefaultAsync();
                return usuario != null? usuario: new();
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            try
            {
                List<Usuario> ListaUsuarios = await _dbmantordContext.Usuarios.ToListAsync();
                return ListaUsuarios;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Usuario> Login(string user){
           Usuario usuario = await _dbmantordContext.Usuarios.Where(u => u.Email == user).FirstOrDefaultAsync() ?? new();
           return usuario;    
        }
        public async Task<bool> SignOff(string user,  string password){
            Usuario usuario = await _dbmantordContext.Usuarios.Where(u => u.Email == user && u.Contrasena == password).FirstOrDefaultAsync() ?? new();
            bool rpta = string.IsNullOrEmpty(usuario.Email);
           
           return rpta;
        }

    }
}