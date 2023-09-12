using AutoMapper;
using Identity.Framework.Data.Entities;
using Identity.Framework.Domain;
using Identity.Framework.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Mappers {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<UserCredentialWithProfile, UserCredentialEntity>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(o => o.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(o => o.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(o => o.Password));

            CreateMap<UserCredentialWithProfile, UserProfileEntity>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(o => o.UserId))
                .ForMember(dest => dest.FullName, src => src.MapFrom(o => o.FullName))
                .ForMember(dest => dest.Dob, src => src.MapFrom(o => o.Dob));

            CreateMap<UserCredentialWithProfile, UserBankAccountEntity>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(o => o.UserId))
                .ForMember(dest => dest.AccountNumber, src => src.MapFrom(o => o.AccountNumber))
                .ForMember(dest => dest.Currency, src => src.MapFrom(o => o.Currency));

            CreateMap<UserCredentialWithProfile, UserCredentialWithProfileResult>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(o => o.UserId))
                .ForMember(dest => dest.Username, src => src.MapFrom(o => o.Username))
                .ForMember(dest => dest.Dob, src => src.MapFrom(o => o.Dob))
                .ForMember(dest => dest.FullName, src => src.MapFrom(o => o.FullName))
                .ForMember(dest => dest.IsSuccessful, src => src.MapFrom(o => o.UserId > 0))
                .ForMember(dest => dest.Message, src => src.MapFrom(o => o.UserId > 0 ? StringResources.CreateSuccessfully : StringResources.CreateFailed));

            CreateMap<UserCredentialEntity, UserLogin>()
                .ForMember(dest => dest.AccountNumber, src => src.MapFrom(o => o.UserBankAccounts.FirstOrDefault().AccountNumber))
                .ForMember(dest => dest.FullName, src => src.MapFrom(o => o.UserProfile.FullName))
                .ForMember(dest => dest.Currency, src => src.MapFrom(o => o.UserBankAccounts.FirstOrDefault().Currency ?? "IDR"));

        }
    }
}
