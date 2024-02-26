using Depences.Infrastructure.Interfaces.IDataModel;
using System.Linq.Expressions;

namespace Depences.Infrastructure.Interfaces.IRepositories
{
    public interface IReadOnlyEntityRepository<T> : IEntityRepository
     where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize = 10, string? sortExpression = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize = 10, CancellationToken cancellationToken = default);

    }
}
