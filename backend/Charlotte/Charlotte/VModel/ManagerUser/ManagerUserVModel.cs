namespace Charlotte.VModel.ManagerUser
{
    public class ManagerUserVModel
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
    }

    public class ManagerUsersVModel
    {
        public int managerUserId { get; set; }
        public string userName { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
        public string flag { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
