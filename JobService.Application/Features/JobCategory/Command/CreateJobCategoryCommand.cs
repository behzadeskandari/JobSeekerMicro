using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Command
{
    public class CreateJobCategoryCommand : IRequest<Result<int>>
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string NameEn { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}

