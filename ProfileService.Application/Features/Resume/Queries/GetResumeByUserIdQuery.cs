using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Resume;
using MediatR;

namespace ProfileService.Application.Features.Resume.Queries
{
    public class GetResumeByUserIdQuery : IRequest<Result<ResumeDto>>
    {
        public string UserId { get; set; }
    }
}
