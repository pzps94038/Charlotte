namespace Charlotte.Model.User
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; }
        public bool Flag { get; set; }
    }
    public class ModifyUserModel
    {
        public string? UserName { get; set; }
        public string? Account { get; set; }
        public string? password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Flag { get; set; }
    }
}
