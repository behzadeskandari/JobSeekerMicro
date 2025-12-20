using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Resume.Queries
{
    public class GetResumeByIdQuery : IRequest<Result<JobSeeker.Shared.Contracts.Resume.ResumeDto>>
    {
        public Guid Id { get; set; }
    }

}
