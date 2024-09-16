using AutoMapper;
using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Entities.Concretes;

namespace CustomIdentityAuth.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>();
    }
}