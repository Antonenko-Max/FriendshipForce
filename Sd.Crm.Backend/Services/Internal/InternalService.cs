using Microsoft.EntityFrameworkCore;
using Sd.Crm.Backend.Controllers.Requests.Internal;
using Sd.Crm.Backend.DataLayer;
using Sd.Crm.Backend.Exceptions;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.Services.Internal
{
    public class InternalService : IInternalService
    {
        private readonly CrmContext _context;

        public InternalService(CrmContext context)
        {
            _context = context;
        }

        public async Task<DiscipleLevel> CreateLevel(DiscipleLevelRequest request, CancellationToken ct)
        {
            var level = new DiscipleLevel(request.Name);
            var entry = await _context.DiscipleLevels.AddAsync(level, ct);
            await _context.SaveChangesAsync(ct);
            return entry.Entity;
        }

        public async Task<SdProject> CreateSdProject(SdProjectCreateRequest request, CancellationToken ct)
        {
            var project = new SdProject(request.Name);
            var entry = await _context.SdProjects.AddAsync(project, ct);
            await _context.SaveChangesAsync(ct);
            return entry.Entity;
        }

        public async Task<DiscipleLevel> DeleteLevel(Guid id, CancellationToken ct)
        {
            var level = await _context.DiscipleLevels.FindAsync(id, ct);
            if (level == null)
            {
                throw new NotFoundException($"Level {id} not found");
            }

            _context.DiscipleLevels.Remove(level);
            await _context.SaveChangesAsync(ct);
            return level;
        }

        public async Task<SdProject> DeleteSdProject(Guid projectId, CancellationToken ct)
        {
            var project = await _context.SdProjects.FindAsync(projectId, ct);
            if (project == null)
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            _context.SdProjects.Remove(project);
            await _context.SaveChangesAsync(ct);
            return project;
        }

        public async Task<DiscipleLevel> GetLevel(Guid id, CancellationToken ct)
        {
            var level = await _context.DiscipleLevels.FindAsync(id, ct);
            if (level == null)
            {
                throw new NotFoundException($"Level {id} not found");
            }
            return level;
        }

        public async Task<DiscipleLevel> GetLevelByName(string name, CancellationToken ct)
        {
            var level = await _context.DiscipleLevels.FirstOrDefaultAsync(l => l.Name == name, ct);
            if (level == null)
            {
                throw new NotFoundException($"Level {name} not found");
            }
            return level;
        }

        public async Task<IEnumerable<DiscipleLevel>> GetLevels(CancellationToken ct)
        {
            var levels = await _context.DiscipleLevels.ToArrayAsync(ct);
            return levels;
        }

        public async Task<SdProject> GetSdProject(Guid projectId, CancellationToken ct)
        {
            var project = await _context.SdProjects.FindAsync(projectId, ct);
            if (project == null)
            {
                throw new NotFoundException($"Project {projectId} not found");
            }
            return project;
        }

        public async Task<SdProject> GetSdProjectByName(string name, CancellationToken ct)
        {
            var project = await _context.SdProjects.FirstOrDefaultAsync(p => p.Name == name, ct);
            if (project == null)
            {
                throw new NotFoundException($"Project {name} not found");
            }
            return project;
        }

        public async Task<IEnumerable<SdProject>> GetSdProjects(CancellationToken ct)
        {
            var projects = await _context.SdProjects.ToArrayAsync(ct);
            return projects;
        }

        public bool TryGetLevelByName(string name, out DiscipleLevel? level)
        {
            level = _context.DiscipleLevels.FirstOrDefault(l => l.Name == name);
            return level == null ? false : true;
        }

        public async Task<DiscipleLevel> UpdateLevel(Guid id, DiscipleLevelRequest request, CancellationToken ct)
        {
            var level = await _context.DiscipleLevels.FindAsync(id, ct);
            if (level == null)
            {
                throw new NotFoundException($"Level {id} not found");
            }

            level.Name = request.Name;
            var entry = _context.DiscipleLevels.Update(level);
            await _context.SaveChangesAsync(ct);
            return entry.Entity;
        }

        public async Task<SdProject> UpdateSdProject(Guid projectId, SdProjectCreateRequest request, CancellationToken ct)
        {
            var project = await _context.SdProjects.FindAsync(projectId, ct);
            if (project == null)
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            project.Name = request.Name;
            var entry = _context.SdProjects.Update(project);
            await _context.SaveChangesAsync(ct);
            return entry.Entity;
        }

        public async Task<SdProject> UseProjectFallback(CancellationToken ct)
        {
            var defaultProject = "Сила Дружбы";

            var project = await _context.SdProjects.FirstOrDefaultAsync(p => p.Name == defaultProject, ct);
            if (project == null)
            {
                project = new SdProject(defaultProject);
                _context.SdProjects.Add(project);
                await _context.SaveChangesAsync(ct);
            }

            return project;
        }
    }
}
