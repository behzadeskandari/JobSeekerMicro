using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.UserSetting;
using MediatR;
using ProfileService.Application.Features.UserSettings.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.UserSettings.Handlers
{
    public class GetUserSettingsByIdQueryHandler : IRequestHandler<GetUserSettingsByIdQuery, Result<UserSettingDto>>
    {
        private readonly IProfileServiceUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetUserSettingsByIdQueryHandler(IProfileServiceUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UserSettingDto>> Handle(GetUserSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var userSetting = await _repository.UserSettingsRepository.GetByIdAsync(request.UserId);

            if (userSetting == null)
            {
                return Result.Fail<UserSettingDto>($"User settings not found for user ID: {request.UserId}");
            }

            var dto = _mapper.Map<UserSettingDto>(userSetting);
            return Result.Ok(dto);
        }
    }
}
