using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Services;
using FluentValidation.Results;

namespace Core.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDTO>()
        .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
        .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone));

        CreateMap<InsertAccountRequest, Account>()
        // Normalize Address {Beazley: ToUpper() Pro"}
        .ForMember(d => d.Address, o => o.MapFrom(s => "Beazley: " + s.Address.ToUpper() + " Pro"))
        .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone));

        CreateMap<ValidationFailure, AccontErrorDTO>()
        .ForMember(d => d.FieldName, o => o.MapFrom(s => s.PropertyName))
        .ForMember(d => d.ErrorMessage, o => o.MapFrom(s => s.ErrorMessage));
    }
}