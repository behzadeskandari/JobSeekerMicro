using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Command
{
    public class UpdateProvinceCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? Value { get; set; }
    }
}

