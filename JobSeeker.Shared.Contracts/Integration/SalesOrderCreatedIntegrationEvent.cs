namespace JobSeeker.Shared.Contracts.Integration
{
    public class SalesOrderCreatedIntegrationEvent(
        Guid SalesOrderId,
        int CustomerId,
        bool IsPaid
    ) : IntegrationEvent;
}

