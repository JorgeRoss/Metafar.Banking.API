using AutoMapper;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Domain.Entities;

namespace Metafar.Banking.Application.UseCases.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<Card, CardTokenDto>().ReverseMap();
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.LastExtractionDate, opt => opt.MapFrom(src => src.LastExtractionDate));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionTypeId))
                .ReverseMap();
        }
    }
}
