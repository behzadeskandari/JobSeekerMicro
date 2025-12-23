using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.UserSetting;
using MediatR;

namespace ProfileService.Application.Features.UserSettings.Queries
{
    public record GetUserSettingsByIdQuery(Guid UserId) : IRequest<Result<UserSettingDto>>;
}
