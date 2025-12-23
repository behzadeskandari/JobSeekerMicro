using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Roles
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";

        public static class Combinations
        {
            public const string UserOrStaff = $"{User},{Staff}";
            public const string StaffOrAdmin = $"{Staff},{Admin}";
            public const string UserOrStaffOrAdmin = $"{User},{Staff},{Admin}";
        }
    }
}
