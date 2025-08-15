using AutoMapper;
using GymGo.Application.Dtos;
using GymGo.Domain.Entities;

namespace GymGo.Application.Profiles
{
    public class MappingProfile
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, ClientDetailDto>().ReverseMap();
            CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();
            CreateMap<MembershipType, MembershipTypeDetailDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Auditoria.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Auditoria.CreatedDate))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.Auditoria.LastModifiedBy))
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.Auditoria.LastModifiedDate))
                .ReverseMap();
        }
    }
}
