using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Resume;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.Resume.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, Result<ResumeDto>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetResumeByIdQueryHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ResumeDto>> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.ResumeRepository.GetByIdAsync(request.Id);

            if (record == null)
            {
                return new Result<ResumeDto>().WithError("Resume Record NotFound404");
            }
            else
            {
                var resumeDto = CreateResumeDto(record);
                return Result.Ok(resumeDto);
            }
        }
        private static ResumeDto CreateResumeDto(Domain.Entities.Resume record)
        {
            var resumeDto = new ResumeDto
            {
                Address = record.Address,
                Educations = record.Educations.Select(edu => new EducationDto
                {
                    Degree = edu.Degree,
                    Description = edu.Description,
                    EndDate = edu.EndDate,
                    Field = edu.Field,
                    Institution = edu.Institution,
                    ResumeId = edu.ResumeId,
                    StartDate = edu.StartDate,
                }).ToList(),
                WorkExperiences = record.WorkExperiences.Select(work => new WorkExperienceDto
                {
                    Description = work.Description,
                    EndDate = work.EndDate,
                    ResumeId = work.ResumeId,
                    StartDate = work.StartDate,
                    CompanyName = work.CompanyName,
                    IsCurrentJob = work.IsCurrentJob,
                    JobTitle = work.JobTitle,
                }).ToList(),
                Skills = record.Skills.Select(skill => new SkillDto
                {
                    Name = skill.Name,
                    ResumeId = skill.ResumeId,
                    ProficiencyLevel = skill.ProficiencyLevel
                }).ToList(),
                Languages = record.Languages.Select(language => new LanguageDto
                {
                    Name = language.Name,
                    ProficiencyLevel = language.ProficiencyLevel,
                    ResumeId = language.ResumeId,
                }).ToList(),
                FullName = record.FullName,
                Email = record.Email,
                Phone = record.Phone,
                ProfilePictureUrl = record.ProfilePictureUrl,
                Summary = record.Summary,
                UserId = record.UserId,
            };
            return resumeDto;
        }
    }
}
