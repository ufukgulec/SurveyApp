using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(SurveyAppContext context) : base(context)
        {
        }
        public IQueryable<Country> OrderedByUserCount(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query.OrderByDescending(x => x.Profiles.Count);
        }
        Country GetByCode(string Code, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            var country = query.FirstOrDefault(x => x.Code == Code);
            if (country != null)
            {
                return country;
            }
            else
            {
                return null;
            }


        }
    }
}
