using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Command
{
    public class DeleteJobCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

