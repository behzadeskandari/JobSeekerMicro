using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Command
{
    public class DeleteAdvertisementCommand : IRequest<FluentResults.Result>
    {
        public Guid Id { get; set; }
    }
}
