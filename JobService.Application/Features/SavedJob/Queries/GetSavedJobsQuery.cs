using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SavedJob.Queries
{
    public class GetSavedJobsQuery : IRequest<IEnumerable<JobService.Domain.Entities.SavedJob>>
    {
        public string? UserId { get; set; }
        public int? JobPostId { get; set; }
    }
}

