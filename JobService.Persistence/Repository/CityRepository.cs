using JobSeeker.Shared.Common.Interfaces;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.Repository
{
    public class CityRepository : GenericWriteRepository<City>, ICityRepository
    {
        private readonly JobDbContext _context;
        public CityRepository(JobDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateCityAsync(City city, int provinceId)
        {
            var record = await _context.Cities.FirstOrDefaultAsync(x => x.Id == city.Id);
            Province province = null;
            if (record != null)
            {
                if (city.ProvinceId != null)
                {
                    province = await _context.Provinces.FirstOrDefaultAsync(x => x.Id == provinceId);
                    if (province == null)
                    {
                        return;
                    }
                    else
                    {
                    }
                }
                else
                {
                }
                record.Label = city.Label;
                record.ProvinceId = city.ProvinceId;
                record.IsActive = city.IsActive;
                record.Value = city.Value.ToString();

                _context.Cities.Update(record);
            }
            else
            {
                return;
            }
        }
    }
}
