using IceCreamEShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IceCreamEShop.Core.Enums;
using IceCreamEShop.Core.DTOs.UserAccount;

namespace IceCreamEShop.Service.Profiles
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, LoginResponseDto>()
            .ForMember(dto => dto.UserRole,
            tool => tool.MapFrom(ent => ((UserRole)ent.UserRole).ToString()));
            CreateMap<UserAccount, UserDto>()
            .ForMember(dto => dto.UserRole,
            tool => tool.MapFrom(ent => ((UserRole)ent.UserRole).ToString()));
            CreateMap<RegisterRequestDto, UserAccount>();
        }
    }
}
