using AutoMapper;
using Depences.Application.IMangers;

namespace Depences.Application.Managers
{
    public abstract class BaseManager : IBaseManager
    {
        protected IMapper Mapper { get; private set; }
        protected BaseManager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
