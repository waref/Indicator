using AutoMapper;
using Depences.Application.IMangers;
using Depences.Domain.Enums;
using Depences.Domain.Models;
using Depences.Infrastructure.Implementation.DataModels;
using Depences.Infrastructure.Interfaces.IRepositories;

namespace Depences.Application.Managers
{
    public class DepenceManager : BaseManager, IDepenceManager<Depence>
    {
        private readonly IDepenceRepository _depenceRepository;

        public DepenceManager(IMapper mapper, IDepenceRepository depenceRepository) : base(mapper)
        {
            _depenceRepository = depenceRepository;
        }

        public bool IsCurrencyMatchingAsync(Depence depenceRequest, CancellationToken cancellationToken = default)
        {
            int? currencyID = _depenceRepository.GetUserCurrencyID(depenceRequest.UserId);
            return currencyID == depenceRequest.CurrencyId;

        }

        public async Task<ICollection<Depence>> FindADuplicatedsync(Depence depenceRequest,CancellationToken cancellationToken = default)
        {
            var dbResult = await _depenceRepository.FindAsync(i => i.Montant == depenceRequest.Montant && i.DepenceDate == depenceRequest.DepenceDate, 0, 1, cancellationToken);

            var result = dbResult.Select(i => Mapper.Map<Depence>(i)).ToList();


            return result.ToList();
        }
        public async Task<ExecutionResult> SeveDepenceAsync(Depence depenceRequest)          
        {
            try
            {
                var entity = Mapper.Map<DepenceEntity>(depenceRequest);
                await _depenceRepository.CreateAsync(entity);
                await _depenceRepository.SaveChangesAsync();
                return new ExecutionResult(ExecutionStatus.Success, Origin.DepencesModule, "Save");
            }catch (Exception ex)
            {
                return new ExecutionResult(ExecutionStatus.Error, Origin.DepencesModule, ex.Message);
            }

        }

        public async Task<ICollection<Depence>> GetAllCustomAsync(int pageIndex = 0, int pageSize = 10, string? sortExpression = null, CancellationToken cancellationToken = default)
        {
            IEnumerable<DepenceEntity> resultEntities = await _depenceRepository.GetAllAsync(pageIndex, pageSize, sortExpression, cancellationToken);

            var result =  resultEntities.Select(i => Mapper.Map<Depence>(i)).ToList();

            return result.ConvertAll(x => new Depence(x.DepenceId, x.User, x.Nature, x.Montant, x.Commentaire, x.DepenceDate,x.CurrencyId));
        }

        public bool IsExistingNature(int? natureID)
        {
            return  _depenceRepository.IsExistingNature(natureID);
        }

        public async Task<ICollection<Depence>> GetDepenceAsync(string? sortExpression)
        {
            IEnumerable<DepenceEntity> resultEntities = await _depenceRepository.GetAllAsync(sortExpression );

            var result = resultEntities.Select(i => Mapper.Map<Depence>(i)).ToList();
            return result;
        }
    }
}
