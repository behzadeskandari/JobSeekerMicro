using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobRequest.Command
{
    public class DeleteJobRequestCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

