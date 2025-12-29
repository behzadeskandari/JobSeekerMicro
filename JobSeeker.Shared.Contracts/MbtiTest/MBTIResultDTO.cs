using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.User;

namespace JobSeeker.Shared.Contracts.MbtiTest
{
    public class MBTIResultDTO
    {
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;// New field for calculation result 
        public bool? IsActive { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public UserDto? Users { get; set; }
    }
}
