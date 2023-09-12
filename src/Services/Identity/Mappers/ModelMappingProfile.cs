using AutoMapper;
using Identity.Framework.Domain;
using Identity.WebApi.Models;

namespace Identity.WebApi.Mappers {
    public class ModelMappingProfile : Profile {
        public ModelMappingProfile() {
            CreateMap<UserCredentialWithProfileModel, UserCredentialWithProfile>()
                .ForMember(dest => dest.Username, src => src.MapFrom(o => o.UserAccount.Username))
                .ForMember(dest => dest.FullName, src => src.MapFrom(o => o.FullName))
                .ForMember(dest => dest.Password, src => src.MapFrom(o => o.UserAccount.Password))
                .ForMember(dest => dest.Dob, src => src.MapFrom(o => o.Dob))
                .ForMember(dest => dest.AccountNumber, src => src.MapFrom(o => o.BankAccount.AccountNumber))
                .ForMember(dest => dest.Deposit, src => src.MapFrom(o => o.BankAccount.Deposit))
                .ForMember(dest => dest.Currency, src => src.MapFrom(o => o.BankAccount.Currency))
                .ForMember(dest => dest.UserId, src => src.Ignore());
        }
    }
}
