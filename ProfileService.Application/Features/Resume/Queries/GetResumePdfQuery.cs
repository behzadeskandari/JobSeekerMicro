using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Resume.Queries
{
    public class GetResumePdfQuery : IRequest<Result<byte[]>>
    {
        public Guid Id { get; set; }
    }
}
