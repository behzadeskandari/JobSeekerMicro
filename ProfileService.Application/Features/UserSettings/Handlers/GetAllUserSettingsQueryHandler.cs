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
    public class GetAllUserSettingsQueryHandler : IRequestHandler<GetAllUserSettingsQuery, Result<List<UserSettingDto>>>
    {
        private readonly IProfileServiceUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllUserSettingsQueryHandler(IProfileServiceUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<UserSettingDto>>> Handle(GetAllUserSettingsQuery request, CancellationToken cancellationToken)
        {
            var userSettings = await _repository.UserSettingsRepository.GetAllAsync(cancellationToken);

            if (userSettings == null || !userSettings.Any())
            {
                return Result.Ok(new List<UserSettingDto>());
            }

            var dtos = _mapper.Map<List<UserSettingDto>>(userSettings);
            return Result.Ok(dtos);
        }
    }
}
