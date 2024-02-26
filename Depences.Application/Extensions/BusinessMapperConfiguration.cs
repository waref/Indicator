using AutoMapper;
using Depences.Domain.Models;
using Depences.Infrastructure.Implementation.DataModels;

namespace Depences.Application.Extensions
{
    public class BusinessMapperConfiguration : Profile
    {
        public BusinessMapperConfiguration()
        {
            AllowNullCollections = true;
            CreateMap<DeviseEntity, Devise>().ReverseMap();

            CreateMap<NatureEntity, Nature>()
                .ReverseMap();

            CreateMap<UserEntity, User>()
                .ReverseMap();

            CreateMap<DepenceEntity, Depence>()
                .ReverseMap();
            //Association
            CreateMap<UserDeviseEntity, UserDevise>()
               .ReverseMap();

        }
    }

}
