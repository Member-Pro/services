using System;
using System.Linq;
using System.Threading.Tasks;
using MemberPro.Core.Data.Implementations;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Services.Common;
using MemberPro.Core.Services.Organizations;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MemberPro.Core.Services.Tests.Organizations
{
    public class OrganizationServiceTests : ServiceTestBase
    {
        protected override void InitDatabase()
        {
            base.InitDatabase();

            var nationalOrg = new Organization
            {
                Id = 1,
                Parent = null,
                Name = "National Org",
                Abbreviation = "NAT",
                CreatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
            };

            DbContext.Set<Organization>().Add(nationalOrg);

            var regionalOrg1 = new Organization
            {
                Id = 2,
                Parent = nationalOrg,
                Name = "Regional Org 1",
                Abbreviation = "REGION1",
                CreatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
            };

            DbContext.Set<Organization>().Add(regionalOrg1);

            var regionalOrg2 = new Organization
            {
                Id = 3,
                Parent = nationalOrg,
                Name = "Regional Org 2",
                Abbreviation = "REGION2",
                CreatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
            };

            DbContext.Set<Organization>().Add(regionalOrg2);

            var divisionOrg1 = new Organization
            {
                Id = 4,
                Parent = regionalOrg1,
                Name = "Division Org 1",
                Abbreviation = "DIVISION1",
                CreatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
            };

            DbContext.Set<Organization>().Add(divisionOrg1);

            var divisionOrg2 = new Organization
            {
                Id = 5,
                Parent = regionalOrg2,
                Name = "Division Org 2",
                Abbreviation = "DIVISION2",
                CreatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2021, 11, 1, 0, 0, 0, DateTimeKind.Utc),
            };

            DbContext.Set<Organization>().Add(divisionOrg2);

            DbContext.SaveChanges();
        }

        [Fact]
        public async Task GetOrganizationWithParents_ReturnsCorrectResults_ForRegionalLevel()
        {
            // Arrange
            var dateTimeServiceMock = new Mock<IDateTimeService>();
            dateTimeServiceMock.SetupGet(x => x.NowUtc).Returns(new DateTime(2021, 11, 5, 0, 0, 0));

            var repository = new EfRepository<Organization>(DbContext);

            var loggerMock = new Mock<ILogger<OrganizationService>>();

            var organizationService = new OrganizationService(dateTimeServiceMock.Object,
                repository,
                Mapper,
                loggerMock.Object);

            // Act
            var organizations = (await organizationService.GetOrganizationWithParents(2)).ToList();

            // Assert
            Assert.NotEmpty(organizations);
            Assert.Equal(2, organizations.Count());

            var regionOrg = organizations[0];

            Assert.Equal("Regional Org 1", regionOrg.Name);
            Assert.NotNull(regionOrg.Parent);
            Assert.Equal(1, regionOrg.ParentId);

            var nationalOrg = organizations[1];

            Assert.Equal("National Org", nationalOrg.Name);
            Assert.Null(nationalOrg.Parent);
        }

        [Fact]
        public async Task GetOrganizationWithParents_ReturnsCorrectResults_ForDivisionLevel()
        {
            // Arrange
            var dateTimeServiceMock = new Mock<IDateTimeService>();
            dateTimeServiceMock.SetupGet(x => x.NowUtc).Returns(new DateTime(2021, 11, 5, 0, 0, 0));

            var repository = new EfRepository<Organization>(DbContext);

            var loggerMock = new Mock<ILogger<OrganizationService>>();

            var organizationService = new OrganizationService(dateTimeServiceMock.Object,
                repository,
                Mapper,
                loggerMock.Object);

            // Act
            var organizations = (await organizationService.GetOrganizationWithParents(4)).ToList();

            // Assert
            Assert.NotEmpty(organizations);
            Assert.Equal(3, organizations.Count());

            var divisionOrg = organizations[0];

            Assert.Equal("Division Org 1", divisionOrg.Name);
            Assert.NotNull(divisionOrg.Parent);
            Assert.Equal(2, divisionOrg.ParentId);

            var regionOrg = organizations[1];

            Assert.Equal("Regional Org 1", regionOrg.Name);
            Assert.NotNull(regionOrg.Parent);
            Assert.Equal(1, regionOrg.ParentId);

            var nationalOrg = organizations[2];

            Assert.Equal("National Org", nationalOrg.Name);
            Assert.Null(nationalOrg.Parent);
        }
    }
}
