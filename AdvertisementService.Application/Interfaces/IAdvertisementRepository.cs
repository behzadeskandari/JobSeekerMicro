using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;

namespace AdvertisementService.Application.Interfaces
{
    public interface IAdvertisementRepository : IWriteRepository<Advertisement> 
    {
    }
}
