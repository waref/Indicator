

using AutoMapper;
using Depences.Domain.Models;
using Depences.Infrastructure.Implementation.DataModels;

namespace Depences.Extensions.Extensions
{
    public class BusinessMapperConfiguration : Profile
    {
        public BusinessMapperConfiguration()
        {
            AllowNullCollections = true;
            CreateMap<DepenceEntity, Depence>().ReverseMap();
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<DeviseEntity, Devise>().ReverseMap();
            CreateMap<NatureEntity, Nature>().ReverseMap();



        }
    }

}
