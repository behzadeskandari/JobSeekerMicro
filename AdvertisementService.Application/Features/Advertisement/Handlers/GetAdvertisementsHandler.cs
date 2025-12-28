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
    public class GetAdvertisementsHandler : IRequestHandler<GetAdvertisementsQuery, IEnumerable<AdvertisementService.Domain.Entities.Advertisement>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetAdvertisementsHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.Advertisement>> Handle(GetAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var record = await _repository.AdvertisementRepository.GetAllAsync();
            return record;
        }
    }
}
