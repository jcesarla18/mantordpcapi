using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Implement
{
    public class OrdenadoresService(DbmantordContext dbmantordContext) : IOrdernadoresService
    {
        private readonly DbmantordContext _dbmantordContext = dbmantordContext;
        public async Task<List<Ordenadore>> GetOrdenadoresAsync()
        {
            var ordenadores = await _dbmantordContext.Ordenadores.ToListAsync();
            return ordenadores;
        }
        public async Task<Ordenadore> CreateOrdenadoresAsync(Ordenadore ordenadore)
        {
            _dbmantordContext.Ordenadores.Add(ordenadore);
            await _dbmantordContext.SaveChangesAsync();
            return ordenadore;
        }
        public async Task<Ordenadore> UpdateOrdenadoresAsync(Ordenadore ordenadore)
        {
            _dbmantordContext.Ordenadores.Update(ordenadore);
            await _dbmantordContext.SaveChangesAsync();
            return ordenadore;
        }
        public async Task<bool> DeleteOrdenadoresAsync(Ordenadore ordenadore)
        {
            _dbmantordContext.Ordenadores.Remove(ordenadore);
            await _dbmantordContext.SaveChangesAsync();
            return true;
        }
        public async Task<Ordenadore> FindOrdenadoresIdAsync(int Id)
        {
            Ordenadore ordenadores = await _dbmantordContext.Ordenadores.FindAsync(Id) ?? new();
            return ordenadores;
        }
    }
}