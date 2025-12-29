using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Command
{
    public class CreateCompanyFollowCommand : IRequest<Result<int>>
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int CompanyId { get; set; }

        public int Rating { get; set; }
    }
}

