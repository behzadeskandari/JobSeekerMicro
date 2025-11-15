using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementService.Persistence.DbContexts
{
    public class AdvertismentDbContext : DbContext
    {
        public DbSet<Advertisement> JobPosts { get; set; }
    }
}
