using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace AdvertisementService.Application.Interfaces
{
    public interface IProductInventoryRepository : IWriteRepository<ProductInventory>
    {
    }
}
