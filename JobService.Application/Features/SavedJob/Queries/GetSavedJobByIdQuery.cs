using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SavedJob.Queries
{
    public class GetSavedJobByIdQuery : IRequest<JobService.Domain.Entities.SavedJob?>
    {
        public Guid Id { get; set; }
    }
}

