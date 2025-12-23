using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.UserSetting;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.UserSettings.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.UserSettings.Handlers
{
    public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand, Result<UserSettingDto>>
    {
        private readonly IProfileServiceUnitOfWork _repository;
        private readonly IMapper _mapper;

        public UpdateUserSettingsCommandHandler(IProfileServiceUnitOfWork repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UserSettingDto>> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var userSetting = await _repository.UserSettingsRepository.GetByIdAsync(request.Id);
            if (userSetting == null)
            {
                return Result.Fail<UserSettingDto>("User settings not found.");
            }

            if (request.EmailNotifications.HasValue) userSetting.EmailNotifications = request.EmailNotifications.Value;
            if (request.SmsNotifications.HasValue) userSetting.SmsNotifications = request.SmsNotifications.Value;
            if (request.TwoFactorEnabled.HasValue) userSetting.TwoFactorEnabled = request.TwoFactorEnabled.Value;
            if (request.Language != null) userSetting.Language = request.Language;
            if (request.TimeZone != null) userSetting.TimeZone = request.TimeZone;

            userSetting.DateModified = DateTime.UtcNow;

            await _repository.UserSettingsRepository.UpdateAsync(userSetting);
            await _repository.CommitAsync(cancellationToken);

            var dto = _mapper.Map<UserSettingDto>(userSetting);
            return Result.Ok(dto);
        }
    }
}
