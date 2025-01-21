using Microsoft.EntityFrameworkCore;
using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Controllers.Responses;
using Sd.Crm.Backend.DataLayer;
using Sd.Crm.Backend.Exceptions;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.SquadModels;
using Sd.Crm.Backend.Services.Google;
using Sd.Crm.Backend.Services.Internal;

namespace Sd.Crm.Backend.Services.Squad
{
    public class SquadService : ISquadService
    {
        private readonly IGoogleAccessService _googleAccessService;
        private readonly IInternalService _internalService;
        private readonly CrmContext _context;

        public SquadService(IGoogleAccessService googleAccessService, IInternalService internalService, CrmContext context)
        {
            _googleAccessService = googleAccessService;
            _context = context;
            _internalService = internalService;
        }

        public async Task<SquadResponse> GetSquad(Guid id, CancellationToken ct)
        {
            return (SquadResponse)(await _context.Squads.FindAsync(id, ct));
        }

        public async Task<SquadResponse> LoadSquadFromSpreadSheet(SpreadsheetRequest request, CancellationToken ct)
        {
            var squad = await _googleAccessService.GetSquad(request, ct);

            var mentor = await _context.Users.FindAsync(request.MentorId);
            if (mentor == null)
            {
                throw new NotFoundException($"Mentor {request.MentorId} not found");
            }
            squad.Mentor = mentor;
            squad.City = request.City;
            squad.Location = request.Location;
            squad.Name = request.Name;

            return squad;
        }

        public async Task<IEnumerable<Model.SquadModels.Squad>> GetSquadsByMentor(Guid id, CancellationToken ct)
        {
            var squads = await _context.Squads
                .Include(s => s.Mentor)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Trainings)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Project)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Mother)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Father)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Level)
                .Where(s => s.Mentor.Id == id).ToListAsync();

            return squads;
        }

        public async Task<IEnumerable<Model.SquadModels.Squad>> GetSquadsByCity(string city, CancellationToken ct)
        {
            var squads = await _context.Squads
                .Include(s => s.Mentor)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Trainings)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Project)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Mother)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Father)
                .Include(s => s.Disciples)
                    .ThenInclude(d => d.Level)
                .Where(s => s.City == city).ToListAsync();

            return squads;
        }

        public async Task<Model.SquadModels.Squad> PostSquad(SquadRequest squadRequest, CancellationToken ct)
        {
            var mothers = new List<Mother>();
            var fathers = new List<Father>();

            var project = await _internalService.UseProjectFallback(ct);

            var squad = await MapSquad(squadRequest);

            foreach (var discipleRequest in squadRequest.Disciples)
            {
                var disciple = discipleRequest.ToEntity();
                if (disciple.Id == null ||  disciple.Id == Guid.Empty) disciple.Id = Guid.NewGuid();

                disciple.Project = project;

                if (discipleRequest.Level != null) disciple.Level = GetLevel(discipleRequest.Level);

                squad.Disciples.Add( disciple );

                await AddMother(discipleRequest, disciple, mothers);
                await AddFather(discipleRequest, disciple, fathers);

                var enumerator = disciple.Trainings.GetEnumerator();
                Training training = null;
                var isCollectionEnded = false;
                while (true)
                {
                    if (!isCollectionEnded && enumerator.MoveNext())
                    {
                        training = enumerator.Current;
                        if (training.Id == null || training.Id == Guid.Empty) training.Id = Guid.NewGuid();

                        training.Disciple = disciple;

                        _context.Trainings.Add(training);
                    }
                    // until the end of May
                    else if ((new DateTime(training.Date.Year, 5, 31, 0, 0, 0) - training.Date).Days >= 7)
                    {
                        isCollectionEnded = true;

                        var nextDate = training.Date + TimeSpan.FromDays(7);

                        training = new Training()
                        {
                            Id = Guid.NewGuid(),
                            Date = nextDate,
                            Month = nextDate.Month,
                            Number = nextDate.Day / 7,
                            Disciple = disciple,
                        };

                        _context.Trainings.Add(training);
                    }
                    // exceeded end of May
                    else
                    { 
                        break;
                    }
                }

                _context.Disciples.Add(disciple);
            }

            var entry = _context.Squads.Add(squad);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        private DiscipleLevel? GetLevel(DiscipleLevel levelRequest)
        {
            if (_internalService.TryGetLevelByName(levelRequest.Name, out var level))
            {
                return level;
            }
            return null;
        }

        private async Task AddMother(DiscipleRequest discipleRequest, Disciple disciple, List<Mother> mothers)
        {
            if (discipleRequest.Mother != null)
            {
                var mother = await _context.Mothers.FirstOrDefaultAsync(m => m.Name == discipleRequest.Mother.Name || m.Phone == discipleRequest.Mother.Phone);

                if (mother != null)
                {
                    disciple.Mother = mother;
                }
                else
                {
                    mother = mothers.FirstOrDefault(m => m.Name == discipleRequest.Mother.Name);
                    if (mother != null) 
                    {
                        disciple.Mother = mother;
                        return;
                    }

                    mother = new Mother()
                    {
                        Id = Guid.NewGuid(),
                        Name = discipleRequest.Mother.Name,
                        Phone = discipleRequest.Mother.Phone,
                        Comment = discipleRequest.Mother.Comment
                    };
                    disciple.Mother = mother;

                    _context.Mothers.Add(mother);
                    mothers.Add(mother);
                }
            }
        }

        private async Task AddFather(DiscipleRequest discipleRequest, Disciple disciple, List<Father> fathers)
        {
            if (discipleRequest.Father != null)
            {
                var father = await _context.Fathers.FirstOrDefaultAsync(m => m.Name == discipleRequest.Father.Name || m.Phone == discipleRequest.Father.Phone);

                if (father != null)
                {
                    disciple.Father = father;
                }
                else
                {
                    father = fathers.FirstOrDefault(m => m.Name == discipleRequest.Father.Name);
                    if (father != null)
                    {
                        disciple.Father = father;
                        return;
                    }

                    father = new Father()
                    {
                        Id = Guid.NewGuid(),
                        Name = discipleRequest.Father.Name,
                        Phone = discipleRequest.Father.Phone,
                        Comment = discipleRequest.Father.Comment
                    };
                    disciple.Father = father;

                    _context.Fathers.Add(father);
                    fathers.Add(father);
                }
            }
        }

        private async Task<Model.SquadModels.Squad> MapSquad(SquadRequest squadRequest)
        {
            var squad = new Model.SquadModels.Squad() { Id = Guid.NewGuid() };

            var mentor = await _context.Users.FindAsync(squadRequest.Mentor.Id);
            if (mentor == null)
            {
                throw new NotFoundException($"mentor {squadRequest.Mentor.Id} not found");
            }

            squad.Mentor = mentor;
            squad.Location = squadRequest.Location;
            squad.City = squadRequest.City;
            squad.Name = squadRequest.Name;

            return squad;
        }
    }
}
