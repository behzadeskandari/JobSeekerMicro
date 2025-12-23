using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.UserSettings.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.UserSettings.Handlers
{
    public class DeleteUserSettingsCommandHandler : IRequestHandler<DeleteUserSettingsCommand, Result>
    {
        private readonly IProfileServiceUnitOfWork _repository;

        public DeleteUserSettingsCommandHandler(IProfileServiceUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var userSetting = await _repository.UserSettingsRepository.GetByIdAsync(request.Id);
            if (userSetting == null)
            {
                return Result.Fail("User settings not found.");
            }

            await _repository.UserSettingsRepository.DeleteAsync(userSetting);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
