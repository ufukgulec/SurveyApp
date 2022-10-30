using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable<Country> OrderedByUserCount(bool tracking = true);
    }
}
