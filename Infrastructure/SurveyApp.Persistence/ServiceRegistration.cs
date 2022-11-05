using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Repositories;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Contexts;
using SurveyApp.Persistence.Repositories;
using SurveyApp.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration["ConnectionString"].ToString();
            services.AddDbContext<SurveyAppContext>(options => options.UseSqlServer(conn));

            //services.AddScoped<ICountryRepository, CountryRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
        //var seedData = new SeedData();
        //await seedData.SeedAsync(configuration);
    }
}

