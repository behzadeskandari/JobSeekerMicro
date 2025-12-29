using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Command
{
    public class UpdateInterviewDetailCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Type { get; set; }
        public string? Notes { get; set; }
        public string? Outcome { get; set; }
    }
}

