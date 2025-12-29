using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Command
{
    public class DeleteProvinceCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

