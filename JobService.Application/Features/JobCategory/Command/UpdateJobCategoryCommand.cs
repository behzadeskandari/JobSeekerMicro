using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Command
{
    public class UpdateJobCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? Slug { get; set; }
        public string? Industry { get; set; }
        public string? Value { get; set; }
    }
}

