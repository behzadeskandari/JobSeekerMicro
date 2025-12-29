using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Company.Queries
{
    public class GetCompanyByIdQuery : IRequest<JobService.Domain.Entities.Company?>
    {
        public int Id { get; set; }
    }
}

