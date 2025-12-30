using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Persistence.DbContexts;
using AdvertisementService.Persistence.Repository;
using Polly;

namespace AdvertisementService.Persistence.UnitOfWork
{
    public class AdvertisementUnitOfWork : IAdvertisementUnitOfWork
    {
        private readonly AdvertismentDbContext _context;
        private readonly IDomainEventDispatcher? _domainEventDispatcher;
        public IAdvertisementRepository _advertisementRepository;
        public IAdvertisementRepository AdvertisementRepository => _advertisementRepository ??= new AdvertisementRepository(_context);


        public ICustomersRepository _customersRepository;
        public ICustomersRepository CustomersRepository => _customersRepository ??= new CustomersRepository(_context);
        

        public ICustomerAddressRepository _customerAddressRepository;
        public ICustomerAddressRepository CustomerAddressRepository => _customerAddressRepository ??= new CustomerAddressRepository(_context);
        

        public IFeatureRepository _featureRepository;
        public IFeatureRepository FeatureRepository => _featureRepository ??= new FeatureRepository(_context);


        public IOrdersRepository _ordersRepository;
        public IOrdersRepository OrdersRepository => _ordersRepository ??= new OrdersRepository(_context);


        public IPaymentRepository _paymentRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository ??= new PaymentRepository(_context);



        public IPricingCategoryRepository _pricingCategoryRepository;
        public IPricingCategoryRepository PricingCategoryRepository => _pricingCategoryRepository ??= new PricingCategoryRepository(_context);



        public IPricingFeaturesRepository _pricingFeaturesRepository;
        public IPricingFeaturesRepository PricingFeaturesRepository => _pricingFeaturesRepository ??= new PricingFeaturesRepository(_context);


        public IPricingPlanRepository _pricingPlanRepository;
        public IPricingPlanRepository PricingPlanRepository => _pricingPlanRepository ??= new PricingPlanRepository(_context);


        public IProductInventoryRepository _productInventoryRepository;
        public IProductInventoryRepository ProductInventoryRepository => _productInventoryRepository ??= new ProductInventoryRepository(_context);


        public IProductRepository _productRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        
        public ISalesOrderItemRepository _salesOrderItemRepository;
        public ISalesOrderItemRepository SalesOrderItemRepository => _salesOrderItemRepository ??= new SalesOrderItemRepository(_context);


        public ISalesOrderRepository _salesOrderRepository;
        public ISalesOrderRepository SalesOrderRepository => _salesOrderRepository ??= new SalesOrderRepository(_context);

        public ILogRepository _logs;
        public ILogRepository Logs => _logs ??= new LogRepository(_context);

        public IExceptionLogRepository _exceptionLogs;
        public IExceptionLogRepository ExceptionLogs => _exceptionLogs ??= new ExceptionLogRepository(_context);

        public AdvertisementUnitOfWork(AdvertismentDbContext context, IDomainEventDispatcher? domainEventDispatcher = null)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            
            // Dispatch domain events after save
            if (_domainEventDispatcher != null)
            {
                await _domainEventDispatcher.DispatchDomainEventsAsync();
                await _context.SaveChangesAsync(cancellationToken); // Save outbox messages
            }
            
            return result;
        }

        public void Dispose() => _context.Dispose();
    }
}
