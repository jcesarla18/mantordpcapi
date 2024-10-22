using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Implement{
    public class ClienteService(DbmantordContext dbmantordContext) : IClienteService
    {
        private readonly DbmantordContext _dbmantordContext = dbmantordContext;
        public async Task<Cliente> CreateClienteAsync(Cliente model)
        {
            _dbmantordContext.Clientes.Add(model);
            await _dbmantordContext.SaveChangesAsync();
            return model;
        }
        public async Task<Cliente> UpdateClienteAsync(Cliente model)
        {
            _dbmantordContext.Clientes.Update(model);
            await _dbmantordContext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteClienteAsync(Cliente model)
        {
            _dbmantordContext.Clientes.Remove(model);
            await _dbmantordContext.SaveChangesAsync();
            return true;
        }

        public Task<Cliente> FindClienteAsync(Cliente model)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> FindClienteIdAsync(int Id)
        {
            try
            {
                Cliente cliente = await _dbmantordContext.Clientes.Where(c => c.ClienteId == Id).FirstAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Cliente>> GetClienteAsync()
        {
            try
            {
                List<Cliente> cliente = await _dbmantordContext.Clientes.ToListAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}