namespace Charlotte.VModel.ManagerUser
{
    public class ManagerUserVModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class ManagerUsersVModel
    {
        public int ManagerUserId { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Flag { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
