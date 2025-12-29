using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Command
{
    public class DeleteInterviewDetailCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

