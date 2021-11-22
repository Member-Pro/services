using AutoMapper;
using MemberPro.Core.Data;
using MemberPro.Core.Data.Implementations;
using MemberPro.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberPro.Core.Services.Tests
{
    public abstract class ServiceTestBase
    {
        protected IDbContext DbContext { get; }
        protected IMapper Mapper { get; }

        protected ServiceTestBase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MemberProDbContext>()
                .UseSqlite("Filename=test.db")
                .Options;

            DbContext = new MemberProDbContext(dbContextOptions);

            var autoMapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new ModelMapper());
            });

            Mapper = autoMapperConfig.CreateMapper();

            InitDatabase();
        }

        protected virtual void InitDatabase()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }
    }
}
