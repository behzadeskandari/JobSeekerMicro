using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Advertisement.Command;
using AdvertisementService.Application.Interfaces;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Handlers
{
    public class DeleteAdvertisementHandler : IRequestHandler<DeleteAdvertisementCommand>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteAdvertisementHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            await _repository.AdvertisementRepository.DeleteAsync(request.Id);
        }
    }
}
