using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Interfaces
{
    public interface IAdvertisementUnitOfWork : IDisposable
    {
        public IAdvertisementRepository AdvertisementRepository { get; }
        public ICustomersRepository CustomersRepository { get; }
        public ICustomerAddressRepository CustomerAddressRepository { get; }
        public IFeatureRepository FeatureRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPricingCategoryRepository PricingCategoryRepository { get; }
        public IPricingFeaturesRepository PricingFeaturesRepository { get; }
        public IPricingPlanRepository PricingPlanRepository { get; }
        public IProductInventoryRepository ProductInventoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ISalesOrderItemRepository SalesOrderItemRepository { get; }
        public ISalesOrderRepository SalesOrderRepository { get; }
        
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
