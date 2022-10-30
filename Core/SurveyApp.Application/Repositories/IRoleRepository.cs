using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        IQueryable<Role> OrderedByUserCount(bool tracking = true);
    }
}
