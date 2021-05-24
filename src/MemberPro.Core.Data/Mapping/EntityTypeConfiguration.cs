using MemberPro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping
{
    // This concept is from NOP Commerce to allow EF to dynamically load 
    // entity configurations
    // https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Data/Mapping/NopEntityTypeConfiguration.cs

    public abstract class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

        }

        public virtual void ApplyConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(this);
        }
    }
}
