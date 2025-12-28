using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Advertisement.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Common.Services;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Handlers
{
    public class CreateAdvertisementHandler : IRequestHandler<CreateAdvertisementCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;
        private readonly ICurrentUserService _currentUserService;

        public CreateAdvertisementHandler(IAdvertisementUnitOfWork repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }
        User user = null;
        public async Task<Result<string>> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            
            if (string.IsNullOrEmpty(_currentUserService.UserEmail) || string.IsNullOrEmpty(_currentUserService.UserId))
            {
                return Result.Fail("شما باید وارد سیستم شوید و ثبت نام کنید و برای تبلیغات در سایت ما یک شرکت ایجاد کنید");
            }
            else
            {
                var userId = _currentUserService.UserId;
                user = await _repository.UsersRepository.GetByIdAsync(userId);
            }

            var category = await _repository.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("دسته بندی صحیح نیست");
            }

            var comapny = await _repository.companyRepository.GetByIdAsync(request.CompanyId);
            if (comapny == null)
            {
                throw new NotFoundException("دسته بندی صحیح نیست");
            }
            if (comapny.UserId != _currentUserService.UserId)
            {
                throw new NotFoundException("شرکت شما نیست");
            }


            var advertisement = new AdvertisementService.Domain.Entities.Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                ExpiresAt = request.ExpiresAt,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                IsApproved = request.IsApproved,
                //Category = category,
                CategoryId = category.Id,
                IsPaid = request.IsPaid,
                JobADVCreatedAt = request.JobADVCreatedAt,
                //Company = comapny,
                CompanyId = comapny.Id,
                //Staff = user,
                //StaffId = user.Id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PaymentId = Guid.Empty,
                //CompanyName = request.CompanyName,
                //PostedDate = request.PostedDate
            };

            await _repository.AdvertisementRepository.AddAsync(advertisement);

            return advertisement.Id;
        }
    }

}
