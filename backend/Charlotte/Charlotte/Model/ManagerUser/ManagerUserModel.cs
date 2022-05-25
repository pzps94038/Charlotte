namespace Charlotte.Model.ManagerUser
{
    public class CreateManagerUserModel
    {
        public string UserName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; }
        public int RoleId { get; set; }
        public bool Flag { get; set; }
    }
    public class ModifyManagerUserModel
    {
        public string? UserName { get; set; }
        public string? Account { get; set; }
        public string? password { get; set; }
        public string? Email { get;  set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int? RoleId { get; set; }
        public bool? Flag { get; set; }
    }
    public class ModifyManagerUserPassWordModel 
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
