using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Repositories;
using SurveyApp.Persistence.Contexts;
using SurveyApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence
{
    public static class ServiceRegistration
    {
        public static async void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration["ConnectionString"].ToString();
            services.AddDbContext<SurveyAppContext>(options => options.UseSqlServer(conn));

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            //var seedData = new SeedData();
            //await seedData.SeedAsync(configuration);
        }
    }
}
