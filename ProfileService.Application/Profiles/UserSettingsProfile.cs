using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobSeeker.Shared.Contracts.UserSetting;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Profiles
{
    public class UserSettingsProfile : Profile
    {
        public UserSettingsProfile()
        {
            CreateMap<UserSetting, UserSettingDto>();
            CreateMap<UserSettingDto, UserSetting>();
        }
    }
}
