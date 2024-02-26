using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Interfaces.IRepositories;
using Depences.Infrastructure.Static;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Depences.Infrastructure.Implementation.Repositories
{
    public abstract class ReadOnlyEntityRepository<T> : IReadOnlyEntityRepository<T>
    where T : class, IEntity
    {
        protected IQueryable<T> EntityAsQueryable { get; init; }
        protected DbContext DbContext { get; init; }
        protected DbSet<T> DbEntitySet { get; init; }
        protected ReadOnlyEntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbEntitySet = DbContext.Set<T>();
            EntityAsQueryable = DbEntitySet.AsQueryable();
        }
        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize = 10, string? sortExpression = null, CancellationToken cancellationToken = default)
        {
            var query = EntityAsQueryable.SortAsQueryable(sortExpression);

            return await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await EntityAsQueryable.Where(filter).Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);
        } 

    }
}
