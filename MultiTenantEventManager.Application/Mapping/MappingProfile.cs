using AutoMapper;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateTenantDto, Tenant>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateEventDto, EventEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateEventDto, EventEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateRegistrationDto, Registration>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
