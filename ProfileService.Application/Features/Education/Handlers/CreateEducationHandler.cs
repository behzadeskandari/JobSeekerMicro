using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Features.Education.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Education.Handlers
{
    public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, Guid>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public CreateEducationHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = new ProfileService.Domain.Entities.Education
            {
                ResumeId = request.ResumeId,
                Degree = request.Degree,
                Institution = request.Institution,
                Field = request.Field,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.EducationRepository.AddAsync(education);
            await _unitOfWork.CommitAsync(cancellationToken);
            return education.Id;
        }
    }
}
