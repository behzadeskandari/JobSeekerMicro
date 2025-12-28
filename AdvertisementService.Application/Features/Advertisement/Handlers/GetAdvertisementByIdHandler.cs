using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Advertisement.Queries;
using AdvertisementService.Application.Interfaces;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Handlers
{
    public class GetAdvertisementByIdHandler : IRequestHandler<GetAdvertisementByIdQuery, AdvertisementService.Domain.Entities.Advertisement>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetAdvertisementByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Advertisement> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AdvertisementRepository.GetByIdAsync(request.Id);
        }
    }
}
