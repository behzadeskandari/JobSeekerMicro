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
    public record CreateUserSettingsCommand(
    string UserId,
    bool EmailNotifications,
    bool SmsNotifications,
    bool TwoFactorEnabled,
    string? Language = "en-US",
    string? TimeZone = "UTC"
) : IRequest<Result<UserSettingDto>>;
}
