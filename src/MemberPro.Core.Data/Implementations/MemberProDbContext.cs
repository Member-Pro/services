using System;
using System.Linq;
using System.Reflection;
using MemberPro.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MemberPro.Core.Data.Implementations
{
    public class MemberProDbContext : DbContext, IDbContext
    {
        public new DatabaseFacade Database => base.Database;

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, new() => base.Set<TEntity>();

        public MemberProDbContext(DbContextOptions<MemberProDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => (x.BaseType?.IsGenericType ?? false)
                    && x.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var config in typeConfigurations)
            {
                var typeConfiguration = (IMappingConfiguration)Activator.CreateInstance(config);
                typeConfiguration.ApplyConfiguration(builder);
            }
        }
    }
}
