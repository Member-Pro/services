using System;
using System.Threading.Tasks;
using MemberPro.Core.Data.Implementations;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Entities.Plans;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Models.Plans;
using MemberPro.Core.Services.Members;
using MemberPro.Core.Services.Plans;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MemberPro.Core.Services.Tests.Members
{
    public class MemberRenewalServiceTests : ServiceTestBase
    {
        public MemberRenewalServiceTests() : base()
        {
        }

        protected override void InitDatabase()
        {
            base.InitDatabase();

            var country = new Country
            {
                Name = "United States",
                Abbreviation = "US",
            };

            DbContext.Set<Country>().Add(country);

            var state = new StateProvince
            {
                Country = country,
                Name = "Wisconsin",
                Abbreviation = "WI",
            };

            DbContext.Set<StateProvince>().Add(state);

            var plan = new MembershipPlan
            {
                Name = "Test 1",
                SKU = "TEST1",
                Description = "Test Plan",
                AvailableStartDate = new DateTime(2021, 10, 1),
                AvailableEndDate = null,
                DurationInMonths = 12,
                Price = 100.00m,
                CreatedOn = new DateTime(2021, 1, 1),
                UpdatedOn = new DateTime(2021, 1, 1),
            };

            DbContext.Set<MembershipPlan>().Add(plan);

            var member = new Member
            {
                FirstName = "Test",
                LastName = "Member",
                EmailAddress = "test@domain.com",
                CurrentPlan = plan,
                Country = country,
                StateProvince = state,
            };

            DbContext.Set<Member>().Add(member);

            var renewal = new MemberRenewal
            {
                Member = member,
                Plan = plan,
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2022, 10, 31, 23, 59, 59),
                PaidDate = new DateTime(2021, 11, 1),
                PaidAmount = 100.00m,
                TransactionId = "TEST001",
                Comments = "Test",
            };

            DbContext.Set<MemberRenewal>().Add(renewal);

            DbContext.SaveChanges();
        }

        // [Fact]
        public async Task GetCurrentRenewalForMemberAsync_ReturnsRenewal_GivenValidDate()
        {
            var plan = new MembershipPlanModel
            {
                Id = 1,
                Name = "Test 1",
                SKU = "TEST1",
                Description = "Test Plan",
                AvailableStartDate = new DateTime(2021, 10, 1),
                AvailableEndDate = null,
                DurationInMonths = 12,
                Price = 100.00m,
                CreatedOn = new DateTime(2021, 1, 1),
                UpdatedOn = new DateTime(2021, 1, 1),
            };

            var member = new MemberModel
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Member",
                EmailAddress = "test@domain.com",
                CurrentPlan = plan,
                // Country = country,
                // StateProvince = state,
            };

            var renewal = new MemberRenewalModel
            {
                Id = 1,
                Member = member,
                Plan = plan,
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2022, 10, 31, 23, 59, 59),
                PaidDate = new DateTime(2021, 11, 1),
                PaidAmount = 100.00m,
                TransactionId = "TEST001",
                Comments = "Test",
            };

            var memberServiceMock = new Mock<IMemberService>();
            memberServiceMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(member);

            var planServiceMock = new Mock<IMembershipPlanService>();
            planServiceMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(plan);

            var loggerMock = new Mock<ILogger<MemberRenewalService>>();

            var memberRenewalService = new MemberRenewalService(memberServiceMock.Object,
                planServiceMock.Object,
                new EfRepository<MemberRenewal>(DbContext),
                loggerMock.Object,
                Mapper);

            var result = await memberRenewalService.CreateRenewalAsync(new CreateRenewalModel
            {
                MemberId = 1,
                PlanId = 1,
                EffectiveDate = new DateTime(2021, 11, 1),
            });


        }


    }
}
