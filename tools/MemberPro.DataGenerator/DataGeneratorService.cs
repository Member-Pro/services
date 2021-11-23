using Bogus;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MemberPro.Tools.DataGenerator
{
    public class DataGeneratorService : IHostedService
    {
        private readonly IDbContext _dbContext;

        public DataGeneratorService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("MemberPro Development Data Generator");
            Console.WriteLine("------------------------------------\n");

            Console.WriteLine("*** To be used for development only! ***\n\n");

            await GenerateMembers(500);

            Console.WriteLine("Data generation complete. Press Ctrl+C to exit");
            Console.ReadKey();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // throw new System.NotImplementedException();
            return Task.CompletedTask;
        }

        private async Task GenerateMembers(int count)
        {
            var countryId = 1; // US only for now

            var divisionOrgIds = await _dbContext.Set<Organization>()
                .Where(x => x.Name.Contains("Division"))
                .Select(x => x.Id)
                .ToListAsync();

            var stateIds = await _dbContext.Set<StateProvince>()
                .Where(x => x.CountryId == countryId)
                .Select(x => x.Id)
                .ToListAsync();

            var userFaker = new Faker<Member>()
                .RuleFor(x => x.FirstName, x => x.Name.FirstName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.Status, x => x.PickRandom<MemberStatus>())
                .RuleFor(x => x.JoinedOn, x => new DateTime(x.Date.Past(20).Ticks, DateTimeKind.Utc))
                .RuleFor(x => x.EmailAddress, x => x.Internet.ExampleEmail())
                .RuleFor(x => x.DateOfBirth, x => new DateTime(x.Date.Past(50).Ticks, DateTimeKind.Utc))
                .RuleFor(x => x.CountryId, countryId)
                .RuleFor(x => x.StateProvinceId, x => x.PickRandom<int>(stateIds))
                .RuleFor(x => x.Address, x => x.Address.StreetAddress())
                .RuleFor(x => x.Address2, x => x.Address.SecondaryAddress())
                .RuleFor(x => x.City, x => x.Address.City())
                .RuleFor(x => x.PostalCode, x => x.Address.ZipCode())
                .RuleFor(X => X.OrganizationId, x => x.PickRandom<int>(divisionOrgIds));

            var members = userFaker.Generate(count);

            _dbContext.Set<Member>().AddRange(members);

            Console.WriteLine($"Generated {count} members...");

            await _dbContext.SaveChangesAsync();
        }

    }
}
