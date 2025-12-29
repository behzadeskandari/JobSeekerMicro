using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Queries
{
    public class GetInterviewDetailsQuery : IRequest<IEnumerable<JobService.Domain.Entities.InterviewDetail>>
    {
        public int? ApplicationId { get; set; }
    }
}

