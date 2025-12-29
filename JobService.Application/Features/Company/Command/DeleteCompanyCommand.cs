using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Company.Command
{
    public class DeleteCompanyCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

