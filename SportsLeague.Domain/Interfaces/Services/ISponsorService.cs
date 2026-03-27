using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsor>> GetAllAsync();
        Task<Sponsor?> GetByIdAsync(int id);
        Task<Sponsor> CreateAsync(Sponsor sponsor);
        Task UpdateAsync(int id, Sponsor sponsor);
        Task DeleteAsync(int id);
        Task JoinedSponsorAsync(int tournamentId, int sponsorId, decimal contractAmount);
        Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId);
        Task RemoveSponsorFromTournamentAsync(int tournamentId, int sponsorId);
    }
}
