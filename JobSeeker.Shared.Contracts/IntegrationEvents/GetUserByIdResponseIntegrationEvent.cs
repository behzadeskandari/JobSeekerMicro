using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class GetUserByIdResponseIntegrationEvent : IntegrationEvent
    {
        public Guid RequestId { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public UserDto? User { get; set; }

        public GetUserByIdResponseIntegrationEvent()
        {
        }

        public GetUserByIdResponseIntegrationEvent(Guid requestId, bool isSuccess, string? errorMessage = null, UserDto? user = null)
        {
            RequestId = requestId;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            User = user;
        }
    }

    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}

