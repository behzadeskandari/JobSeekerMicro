using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Persistence.DbContexts;
using AdvertisementService.Persistence.GenericRepository;

namespace AdvertisementService.Persistence.Repository
{
    public class PaymentRepository : GenericWriteRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AdvertismentDbContext context) : base(context)
        {
        }
    }
}
