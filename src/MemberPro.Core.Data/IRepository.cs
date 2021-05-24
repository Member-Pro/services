using System.Linq;
using System.Threading.Tasks;
using MemberPro.Core.Entities;

namespace MemberPro.Core.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        Task<TEntity> GetByIdAsync(object id);

        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
