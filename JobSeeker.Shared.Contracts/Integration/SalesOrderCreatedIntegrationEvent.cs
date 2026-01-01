namespace JobSeeker.Shared.Contracts.Integration
{
    public class SalesOrderCreatedIntegrationEvent(
        int SalesOrderId,
        int CustomerId,
        bool IsPaid
    ) : IntegrationEvent;
}

