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
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Features.UserSettings.Handlers
{
    public class CreateUserSettingsCommandHandler : IRequestHandler<CreateUserSettingsCommand, Result<UserSettingDto>>
    {
        private readonly IProfileServiceUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CreateUserSettingsCommandHandler(IProfileServiceUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UserSettingDto>> Handle(CreateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.UserSettingsRepository.GetByIdAsync(request.UserId);
            if (existing != null)
            {
                return Result.Fail<UserSettingDto>("User settings already exist for this user.");
            }

            var userSetting = new UserSetting
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                EmailNotifications = request.EmailNotifications,
                SmsNotifications = request.SmsNotifications,
                TwoFactorEnabled = request.TwoFactorEnabled,
                Language = request.Language,
                TimeZone = request.TimeZone,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            // Raise domain event if needed
            //userSetting.Raise(new UserSettingsCreatedEvent(userSetting.Id, userSetting.UserId));

            await _repository.UserSettingsRepository.AddAsync(userSetting);
            await _repository.CommitAsync(cancellationToken);

            var dto = _mapper.Map<UserSettingDto>(userSetting);
            return Result.Ok(dto);
        }
    }
}
