using Microsoft.EntityFrameworkCore;
using Depences.Infrastructure.Interfaces.IRepositories;
using Depences.Infrastructure.Implementation.DataModels;
using Depences.Infrastructure.DBContext;
using Depences.Infrastructure.Static;

namespace Depences.Infrastructure.Implementation.Repositories
{
    public class DepenceRepository : ReadWriteEntityRepository<DepenceEntity>, IDepenceRepository
    {
        private readonly DefaultDbContext _DbContext;
        public DepenceRepository(DefaultDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
            _DbContext.Database.SetCommandTimeout(6000);
        }

        public int? GetUserCurrencyID(int? userId)
        {
            return _DbContext.UsersDepences.Where(i => i.UserId == userId).FirstOrDefault()?.DeviseId;
        }     
          public bool IsExistingNature(int? natureID)
        {
            return _DbContext.Natures.Any(n => n.NatureId == natureID);

        }

        public async Task<List<DepenceEntity>> GetAllAsync(string? sortExpression)
        {
           var query =   _DbContext.Depences
                .Include(u => u.User).ThenInclude(d => d!.Devise).AsQueryable();
            if(sortExpression != null)
            {
                var sortedQuery = query.SortAsQueryable(sortExpression);
                return await sortedQuery.ToListAsync();
            }
            return await query.ToListAsync();


        }
       
    }
}
