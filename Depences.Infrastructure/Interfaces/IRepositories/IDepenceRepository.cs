using Depences.Infrastructure.Implementation.DataModels;

namespace Depences.Infrastructure.Interfaces.IRepositories
{
    public interface IDepenceRepository : IReadWriteEntityRepository<DepenceEntity>
    {
        int? GetUserCurrencyID(int? userId);
        bool IsExistingNature(int? natureID);
        Task<List<DepenceEntity>> GetAllAsync(string? sortExpression = null);
    }
}
