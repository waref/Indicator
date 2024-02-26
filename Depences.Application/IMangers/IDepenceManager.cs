
using Depences.Domain.Models;

namespace Depences.Application.IMangers
{
    public interface IDepenceManager<Depence> : IBaseManager

    {

        Task<ICollection<Depence>> GetDepenceAsync(string? sortExpression = null);
        Task<ExecutionResult> SeveDepenceAsync(Depence depenceRequest);
        Task<ICollection<Depence>> FindADuplicatedsync(Depence depenceRequest,CancellationToken cancellationToken = default);
        bool IsCurrencyMatchingAsync(Depence depenceRequest,CancellationToken cancellationToken = default);
        bool IsExistingNature(int? natureID);


    }
}
