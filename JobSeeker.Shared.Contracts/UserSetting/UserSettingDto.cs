using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.UserSetting
{
    public class UserSettingDto
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Language { get; set; } = "en-US";
        public string TimeZone { get; set; } = "UTC";

        // Navigation property
        //public virtual User? User { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
