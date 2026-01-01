namespace JobSeeker.Shared.Models.Roles
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
