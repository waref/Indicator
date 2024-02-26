using Depences.Infrastructure.Interfaces.IDataModel;
namespace Depences.Infrastructure.Interfaces.IRepositories
{
    public interface IReadWriteEntityRepository<T> : IReadOnlyEntityRepository<T>
    where T : class, IEntity
    {
        Task CreateAsync(T entity);
        Task Update(T entity);
        void Delete(T entity);
        void Clear();
        Task SaveChangesAsync();
    }
}
