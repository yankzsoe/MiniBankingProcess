using AutoMapper;
using Transaction.Framework.Domain;
using Transaction.Framework.Extensions;
using Transaction.Framework.Types;
using Transaction.WebApi.Models;

namespace Transaction.WebApi.Mappers {
    public class ModelMappingProfile : Profile {
        public ModelMappingProfile() {
            CreateMap<TransactionModel, AccountTransaction>()
                .AfterMap<SetIdentityAction>()
                .ForAllMembers(opts => opts.Ignore());

            CreateMap<TransactionResult, TransactionResultModel>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount.ToString("N")))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString()));

            CreateMap<RegisterModel, AccountSummary>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => new Money(o.Balance, o.Currency.TryParseEnum<Currency>())));


            CreateMap<AccountSummary, RegisterModel>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(o => o.AccountNumber));
        }
    }
}
