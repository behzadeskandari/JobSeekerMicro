using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Features;
using JobSeeker.Shared.Common.Interfaces;
using MassTransit.JobService;

namespace AdvertisementService.Application.Interfaces
{
    public interface IFeatureRepository : IWriteRepository<Domain.Entities.Feature>
    {
    }
}
