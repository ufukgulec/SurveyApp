using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence.Contexts
{
    public class SeedData
    {
        private static List<Role> GetRoles()
        {
            List<Role> result = new List<Role>() {
                new Role() { Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="Admin" },
                new Role() { Id=Guid.NewGuid(),CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,Name="User" }
            };
            return result;
        }
        private static List<Country> GetCountries()
        {
            var result = new Faker<Country>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Status, i => i.PickRandom(true, false))
                .RuleFor(i => i.Name, i => i.Address.County())
                .RuleFor(i => i.FlagUrl, i => i.Image.LoremFlickrUrl())
                .RuleFor(i => i.Code, i => i.Address.CountryCode())
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .Generate(20);
            return result;
        }
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Status, i => i.PickRandom(true, false))
                .RuleFor(i => i.Name, i => i.Person.FirstName)
                .RuleFor(i => i.Surname, i => i.Person.LastName)
                .RuleFor(i => i.Password, i => i.Internet.Password())
                .RuleFor(i => i.Email, i => i.Person.Email)
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .Generate(200);
            return result;
        }
        private List<Profile> GetProfiles(List<Guid> rolesIds, List<Guid> countryIds, List<Guid> userIds)
        {
            int counter = 0;
            var result = new Faker<Profile>("tr")
                .RuleFor(i => i.Id, i => userIds[counter++])
                .RuleFor(i => i.CreatedDate, i => DateTime.Now)
                .RuleFor(i => i.Status, i => i.PickRandom(true, false))
                .RuleFor(i => i.CountryId, i => i.PickRandom(countryIds))
                .RuleFor(i => i.RoleId, i => i.PickRandom(rolesIds))
                .RuleFor(i => i.LastLogin, i => DateTime.Now)
                .RuleFor(i => i.Biography, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.ImageUrl, i => i.Person.Avatar)
                .RuleFor(i => i.SecurityQuestion, i => i.Lorem.Sentence(5, 5))
                .RuleFor(i => i.SecurityAnswer, i => i.Lorem.Sentence(5, 5))
                .RuleFor(i => i.UpdatedDate, i => DateTime.Now)
                .Generate(200);
            return result;
        }
        public async Task SeedAsync(IConfiguration configuration)
        {
            var contextBuilder = new DbContextOptionsBuilder();
            contextBuilder.UseSqlServer(configuration["ConnectionString"]);

            var context = new SurveyAppContext(contextBuilder.Options);

            var roles = GetRoles();
            var rolesIds = roles.Select(i => i.Id).ToList();
            await context.Roles.AddRangeAsync(roles);

            var countries = GetCountries();
            var countryIds = countries.Select(i => i.Id).ToList();
            await context.Countries.AddRangeAsync(countries);

            var users = GetUsers();
            var userIds = users.Select(i => i.Id).ToList();
            await context.Users.AddRangeAsync(users);

            var profiles = GetProfiles(rolesIds, countryIds, userIds);
            await context.Profiles.AddRangeAsync(profiles);

            await context.SaveChangesAsync();
        }
    }
}
