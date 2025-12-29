using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class GetUserByIdRequestIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; } = string.Empty;
        public Guid RequestId { get; set; } = Guid.NewGuid();

        public GetUserByIdRequestIntegrationEvent()
        {
        }

        public GetUserByIdRequestIntegrationEvent(string userId, Guid requestId)
        {
            UserId = userId;
            RequestId = requestId;
        }
    }
}

