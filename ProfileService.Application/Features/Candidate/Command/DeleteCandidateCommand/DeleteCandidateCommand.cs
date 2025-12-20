using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Candidate.Command.DeleteCandidateCommand
{
    public class DeleteCandidateCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
