namespace Charlotte.Model.ManagerRoleAuth
{
    public class ManagerRoleAuthModel
    {
        public int RouterId { get; set; }
        public bool ViewAuth { get; set; }
        public bool CreateAuth { get; set; }
        public bool ModifyAuth { get; set; }
        public bool DeleteAuth { get; set; }
        public bool ExportAuth { get; set; }
    }
}
