using System;
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
        private readonly IIdentityServiceClient _identityServiceClient;

        public CreateAdvertisementHandler(
            IAdvertisementUnitOfWork repository, 
            ICurrentUserService currentUserService,
            IIdentityServiceClient identityServiceClient)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _identityServiceClient = identityServiceClient;
        }

        public async Task<Result<string>> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_currentUserService.UserEmail) || string.IsNullOrEmpty(_currentUserService.UserId))
            {
                return Result.Fail("شما باید وارد سیستم شوید و ثبت نام کنید و برای تبلیغات در سایت ما یک شرکت ایجاد کنید");
            }

            var userId = _currentUserService.UserId;

            // Check user existence using synchronous HTTP call
            var userExists = await _identityServiceClient.UserExistsAsync(userId, cancellationToken);
            if (!userExists)
            {
                return Result.Fail("کاربر یافت نشد");
            }

            // Load category (assuming it's in AdvertisementService domain)
            var category = await _repository.PricingCategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return Result.Fail("دسته بندی صحیح نیست");
            }

            // Get company owner using synchronous HTTP call
            var companyOwnerUserId = await _identityServiceClient.GetCompanyOwnerUserIdAsync(request.CompanyId, cancellationToken);
            if (companyOwnerUserId == null)
            {
                return Result.Fail("شرکت یافت نشد");
            }

            // Validate ownership
            if (companyOwnerUserId != userId)
            {
                return Result.Fail("شرکت شما نیست");
            }

            var advertisement = new AdvertisementService.Domain.Entities.Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                ExpiresAt = request.ExpiresAt,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                IsApproved = request.IsApproved,
                CategoryId = category.Id,
                IsPaid = request.IsPaid,
                JobADVCreatedAt = request.JobADVCreatedAt,
                CompanyId = request.CompanyId,
                UserId = userId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PaymentId = Guid.Empty,
            };

            await _repository.AdvertisementRepository.AddAsync(advertisement);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(advertisement.Id.ToString());
        }
    }
}
