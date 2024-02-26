using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Depences.Infrastructure.Implementation.Repositories
{
    public abstract class ReadWriteEntityRepository<T> : ReadOnlyEntityRepository<T>, IReadWriteEntityRepository<T>
  where T : class, IEntity
    {
        protected ReadWriteEntityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateAsync(T entity)
        {
            if (entity != null)
            {
                await DbEntitySet.AddAsync(entity);
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                DbEntitySet.Remove(entity);
            }
        }

        public void Clear()
        {
            DbEntitySet.RemoveRange(DbEntitySet);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity != null)
            {
                DbContext.ChangeTracker.Clear();

                DbContext.Attach(entity);

                DbContext.Entry(entity).State = EntityState.Modified;

                await DbContext.SaveChangesAsync();
            }
        }
    }
}
