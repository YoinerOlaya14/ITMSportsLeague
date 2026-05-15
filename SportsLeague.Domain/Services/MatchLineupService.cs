using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchLineupRepository matchLineupRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            ILogger<MatchLineupService> logger)
        {
            _matchLineupRepository = matchLineupRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<MatchLineup> CreateAsync(int matchId, MatchLineup lineup)
        {
            // V1 - Partido existe
            var match = await _matchRepository.GetByIdAsync(matchId);

            if (match == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");
            }

            // V2 - Jugador existe
            var player = await _playerRepository.GetByIdAsync(lineup.PlayerId);

            if (player == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el jugador con ID {lineup.PlayerId}");
            }

            // V3 - Jugador pertenece al equipo local/visitante
            if (player.TeamId != match.HomeTeamId &&
                player.TeamId != match.AwayTeamId)
            {
                throw new InvalidOperationException(
                    "El jugador no pertenece a ninguno de los equipos del partido");
            }

            // V4 - Prevenir duplicados
            var exists = await _matchLineupRepository
                .ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId);

            if (exists)
            {
                throw new InvalidOperationException(
                    "El jugador ya está registrado en la alineación");
            }

            // V5 - Máximo 11 titulares por equipo
            if (lineup.IsStarter)
            {
                var teamLineups = await _matchLineupRepository
                    .GetByMatchAndTeamAsync(matchId, player.TeamId);

                var startersCount = teamLineups.Count(l => l.IsStarter);

                if (startersCount >= 11)
                {
                    throw new InvalidOperationException(
                        "Un equipo no puede tener más de 11 titulares");
                }
            }

            // V6 - Partido en estado Scheduled
            if (match.Status != MatchStatus.Scheduled)
            {
                throw new InvalidOperationException(
                    "Solo se pueden registrar alineaciones en partidos Scheduled");
            }

            lineup.MatchId = matchId;


            _logger.LogInformation(
                "Adding player {PlayerId} to lineup for match {MatchId}",
                lineup.PlayerId, matchId);

            return await _matchLineupRepository.CreateAsync(lineup);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
        {
            return await _matchLineupRepository.GetByMatchAsync(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId)
        {
            return await _matchLineupRepository
                .GetByMatchAndTeamAsync(matchId, teamId);
        }

        public async Task DeleteAsync(int matchId, int id)
        {
            var lineup = await _matchLineupRepository.GetByIdAsync(id);

            if (lineup == null || lineup.MatchId != matchId)
            {
                throw new KeyNotFoundException(
                    "No se encontró la alineación");
            }

            await _matchLineupRepository.DeleteAsync(id);

            _logger.LogInformation(
                "Removed lineup {LineupId} from match {MatchId}",
                id, matchId);
        }
    }
}