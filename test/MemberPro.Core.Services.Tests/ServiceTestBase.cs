using System;
using System.Data.Common;
using AutoMapper;
using MemberPro.Core.Data;
using MemberPro.Core.Data.Implementations;
using MemberPro.Core.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MemberPro.Core.Services.Tests
{
    public abstract class ServiceTestBase : IDisposable
    {
        private readonly DbConnection _dbConnection;

        protected IDbContext DbContext { get; }
        protected IMapper Mapper { get; }

        protected ServiceTestBase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MemberProDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                // .UseInMemoryDatabase(databaseName: "MemberProTests")
                .Options;

            _dbConnection = RelationalOptionsExtension.Extract(dbContextOptions).Connection;

            DbContext = new MemberProDbContext(dbContextOptions);

            var autoMapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new ModelMapper());
            });

            Mapper = autoMapperConfig.CreateMapper();

            InitDatabase();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

        public void Dispose() => _dbConnection.Dispose();

        protected virtual void InitDatabase()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }
    }
}
