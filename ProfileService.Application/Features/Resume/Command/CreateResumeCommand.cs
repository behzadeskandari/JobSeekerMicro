using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Resume.Command
{
    public class CreateResumeCommand : IRequest<Result<Domain.Entities.Resume>>
    {
        public Domain.Entities.Resume Resume { get; set; }
    }
}
