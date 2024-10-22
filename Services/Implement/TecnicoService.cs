using MantOrdAPI.Models;
using MantOrdAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Services.Implement
{
    public class TecnicoService(DbmantordContext dbmantordContext) : ITecnicoService
    {
        private readonly DbmantordContext _dbmantordContext = dbmantordContext;

        public async Task<List<Tecnico>> GetTecnicoAsync()
        {
            List<Tecnico> tecnicoList = await _dbmantordContext.Tecnicos.ToListAsync();
            return tecnicoList ?? new();
        }
        public async Task<Tecnico> FindTecnicoIdAsync(int Id)
        {
            Tecnico tecnico = await _dbmantordContext.Tecnicos.FindAsync(Id) ?? new();
            return tecnico;
        }
        public async Task<Tecnico> CreateTecnicoAsync(Tecnico tecnico)
        {
            _dbmantordContext.Tecnicos.Add(tecnico);
            await _dbmantordContext.SaveChangesAsync();
            return tecnico;
        }

        public async Task<Tecnico> UpdateTecnicoAsync(Tecnico tecnico)
        {
            _dbmantordContext.Tecnicos.Update(tecnico);
            await _dbmantordContext.SaveChangesAsync();
            return tecnico;
        }

        public async Task<bool> DeleteTecnicoAsync(Tecnico tecnico)
        {
            _dbmantordContext.Tecnicos.Remove(tecnico);
            await _dbmantordContext.SaveChangesAsync();
            return true;
        }
    }
}