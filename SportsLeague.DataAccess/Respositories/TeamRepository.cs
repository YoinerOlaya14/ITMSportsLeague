using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;


namespace SportsLeague.DataAccess.Respositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(LeagueDbContext context) : base(context)
        {
        }


        //Aqui se devuelve un objeto de tipo Team o null si no se encuentra ningún equipo con el nombre especificado.
        public async Task<Team?> GetByNameAsync(string name) 
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }


        //Aqui se devuelve una lista de objetos de tipo Team que pertenecen a la ciudad especificada.
        //Si no se encuentra ningún equipo en esa ciudad, se devuelve una lista vacía.
        public async Task<IEnumerable<Team>> GetByCityAsync(string city) 
        {
            return await _dbSet
                .Where(t => t.City.ToLower() == city.ToLower())
                .ToListAsync();
        }
    }

}
