using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Command
{
    public class CreateInterviewDetailCommand : IRequest<Result<int>>
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public int InterviewerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
        public string Outcome { get; set; } = string.Empty;
    }
}

