using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class ProvinceRepository : GenericWriteRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(JobDbContext context) : base(context)
        {
        }
    }
}
