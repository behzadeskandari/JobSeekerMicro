using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.UserSetting;
using MediatR;

namespace ProfileService.Application.Features.UserSettings.Command
{
    public record UpdateUserSettingsCommand(
      Guid Id,
      bool? EmailNotifications = null,
      bool? SmsNotifications = null,
      bool? TwoFactorEnabled = null,
      string? Language = null,
      string? TimeZone = null
  ) : IRequest<Result<UserSettingDto>>;
}
