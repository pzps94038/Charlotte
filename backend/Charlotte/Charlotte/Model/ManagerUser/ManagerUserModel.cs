namespace Charlotte.Model.ManagerUser
{
    public class CreateManagerUserModel
    {
        public string userName { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
        public int roleId { get; set; }
        public bool flag { get; set; }
    }
    public class ModifyManagerUserModel
    {
        public string? userName { get; set; }
        public string? account { get; set; }
        public string? password { get; set; }
        public string? email { get;  set; }
        public string? address { get; set; }
        public DateTime? birthday { get; set; }
        public int? roleId { get; set; }
        public bool? flag { get; set; }
    }
    public class ModifyManagerUserPassWordModel 
    {
        public string password { get; set; }
        public string newPassword { get; set; }
    }
}
