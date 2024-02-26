
using Depences.Application.IMangers;
using Depences.Domain.Models;
using System.Text;

namespace DepencesApi.BusinessLogic
{
    public class DepencesBusiness : IDisposable
    {
        private readonly IDepenceManager<Depence> _depenceManager;
        private readonly IConfiguration _configuration;

        public DepencesBusiness(IDepenceManager<Depence> depenceManager, IConfiguration configuration)
        {
            _depenceManager = depenceManager;
            _configuration = configuration;
        }
        public async Task<ExecutionResult> Save(Depence depence, CancellationToken cancellationToken)
        {
            ExecutionResult isValidUserDepence = await IsUniqUserDepence(depence, cancellationToken);
            ExecutionResult isValidUserCurrency = IsSameCurrencyDepence(depence, cancellationToken);
            ExecutionResult isValidNature = IsExistingNature(depence.NatureId);

            if (isValidUserCurrency.IsSuccess && isValidUserDepence.IsSuccess && isValidNature.IsSuccess)
            {
                return await _depenceManager.SeveDepenceAsync(depence);
            }
            else
            {

                StringBuilder errorMessage = new StringBuilder();
                errorMessage.AppendLine(isValidUserCurrency.Message);
                errorMessage.AppendLine(isValidUserDepence.Message);
                errorMessage.AppendLine(isValidNature.Message);



                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Error, Depences.Domain.Enums.Origin.DepencesModule, errorMessage.ToString());

            }
        }


        #region Private

        private async Task<ExecutionResult> IsUniqUserDepence(Depence depence,CancellationToken cancellationToken)
        {
            var exists = await _depenceManager.FindADuplicatedsync(depence, cancellationToken);
            if(exists.Any() )
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Error,"Depences existe déjà","");
            }
            else
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Success);
            }


        }
        private ExecutionResult IsSameCurrencyDepence(Depence depence, CancellationToken cancellationToken)
        {
            if( _depenceManager.IsCurrencyMatchingAsync(depence, cancellationToken))
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Success);
            }
            else
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Error, "Devises ne matches pas", "");
            }

        }
        
        private ExecutionResult IsExistingNature(int? natureID)
        {
            if (natureID != 0 && _depenceManager.IsExistingNature(natureID))
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Success);
            }
            else
            {
                return new ExecutionResult(Depences.Domain.Enums.ExecutionStatus.Error, "Nature ID non valide", "");
            }
        }

        #endregion
        void IDisposable.Dispose() { }
    }
}
