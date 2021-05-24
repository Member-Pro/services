using Microsoft.EntityFrameworkCore;

namespace MemberPro.Core.Data.Mapping
{
    // This concept is from NOP Commerce to allow EF to dynamically load 
    // entity configurations
    // https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Data/Mapping/NopEntityTypeConfiguration.cs
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder builder);
    }
}
