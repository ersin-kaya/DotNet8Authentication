using AutoMapper;
using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>();
    }
}