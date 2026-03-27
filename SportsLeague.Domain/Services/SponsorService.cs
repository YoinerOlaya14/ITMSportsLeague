using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Net.Mail;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(ISponsorRepository sponsorRepository,
            ILogger<SponsorService> logger,
            ITournamentSponsorRepository tournamentSponsorRepository,
            ITournamentRepository tournamentRepository)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
            _logger = logger;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sponsors");
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
            var sponsor = await _sponsorRepository.GetByIdAsync(id);

            if (sponsor == null)
                _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);

            return sponsor;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            // Validación de negocio: nombre único
            var existingName= await _sponsorRepository.GetByNameAsync(sponsor.Name);
            if (existingName != null)
            {
                _logger.LogWarning("Sponsor with name '{SponsorName}' already exists", sponsor.Name);
                throw new InvalidOperationException(
                    $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
            }


            try
            {
                var email = new MailAddress(sponsor.ContactEmail);
            }
            catch (FormatException)
            {
                _logger.LogWarning("Invalid email format: '{SponsorContactEmail}'", sponsor.ContactEmail);
                throw new InvalidOperationException("El correo electrónico no tiene un formato válido");
            }

            // 2. Validar duplicado
            var existingContactEmail = await _sponsorRepository.GetByEmailAsync(sponsor.ContactEmail);

            if (existingContactEmail != null)
            {
                _logger.LogWarning("Sponsor with email '{SponsorContactEmail}' already exists", sponsor.ContactEmail);
                throw new InvalidOperationException(
                    $"Ya existe un patrocinador con el email '{sponsor.ContactEmail}'");
            }

            _logger.LogInformation("Creating Sponsor: {SponsorName}", sponsor.Name);
            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existingSponsor = await _sponsorRepository.GetByIdAsync(id);
            if (existingSponsor == null)
            {
                _logger.LogWarning("Sponsor with ID {SponsorId} not found for update", id);
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {id}");
            }

            // Validar nombre único (si cambió)
            if (existingSponsor.Name != sponsor.Name)
            {
                var sponsorWithSameName = await _sponsorRepository.GetByNameAsync(sponsor.Name);
                if (sponsorWithSameName != null)
                {
                    throw new InvalidOperationException(
                        $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
                }
            }


            try
            {
                var email = new MailAddress(sponsor.ContactEmail);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException("El correo electrónico no tiene un formato válido");
            }

            if (existingSponsor.ContactEmail != sponsor.ContactEmail)
            {
                var emailExists = await _sponsorRepository.GetByEmailAsync(sponsor.ContactEmail);
                if (emailExists != null)
                {
                    throw new InvalidOperationException(
                        $"Ya existe un patrocinador con el email '{sponsor.ContactEmail}'");
                }
            }

            existingSponsor.Name = sponsor.Name;
            existingSponsor.ContactEmail = sponsor.ContactEmail;
            existingSponsor.Phone = sponsor.Phone;
            existingSponsor.WebsiteUrl = sponsor.WebsiteUrl;
            existingSponsor.Category = sponsor.Category;

            _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.UpdateAsync(existingSponsor);
        }



        public async Task DeleteAsync(int id)
        {
            var exists = await _sponsorRepository.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Sponsor with ID {SponsorId} not found for deletion", id);
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {id}");
            }

            _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.DeleteAsync(id);
        }

        public async Task JoinedSponsorAsync(int tournamentId, int sponsorId, decimal contractAmount)
        {
            // Validar que el torneo existe
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");

            // Solo se pueden inscribir patrocinadores en torneos Pending
            if (tournament.Status != TournamentStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Solo se pueden inscribir patrocinadores en torneos con estado Pending");
            }


            // Validar que el patrocinador existe
            var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
            if (!sponsorExists)
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {sponsorId}");

            // Validar que no esté ya inscrito
            var existing = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorAsync(tournamentId, sponsorId);

            if (existing != null)
            {
                throw new InvalidOperationException(
                    "Este patrocinador ya está inscrito en el torneo");
            }

            // Validar que el monto del contrato sea positivo
            if (contractAmount <= 0)
            {
                throw new InvalidOperationException("El monto del contrato debe ser mayor a 0");
            }

            var tournamentSponsor = new TournamentSponsor
            {
                TournamentId = tournamentId,
                SponsorId = sponsorId,
                JoinedAt = DateTime.UtcNow,
                ContractAmount = contractAmount
            };

            _logger.LogInformation(
                "Joining sponsor {SponsorId} in tournament {TournamentId}",
                sponsorId, tournamentId);
            await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId)
        {
            var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
            if (!sponsorExists)
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {sponsorId}");

            var relations = await _tournamentSponsorRepository
                .GetBySponsorAsync(sponsorId);

            return relations.Select(ts => ts.Tournament);
        }

        public async Task RemoveSponsorFromTournamentAsync(int tournamentId, int sponsorId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");

            //Solo se pueden remover patrocinadores en torneos Pending
            if (tournament.Status != TournamentStatus.Pending)
            {
                throw new InvalidOperationException(
                    "No se pueden eliminar patrocinadores cuando el torneo no está en estado Pending");
            }

            var existing = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorAsync(tournamentId, sponsorId);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    $"El patrocinador con ID {sponsorId} no está inscrito en el torneo {tournamentId}");
            }

            _logger.LogInformation("Removing sponsor {SponsorId} from tournament {TournamentId} at {Time}",
                sponsorId, tournamentId, DateTime.UtcNow);
            await _tournamentSponsorRepository.DeleteAsync(existing.Id);
        }


    }
}
