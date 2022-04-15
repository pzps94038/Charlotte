namespace Charlotte.Model.ManagerRoleAuth
{
    public class ManagerRoleAuthModel
    {
        public int routerId { get; set; }
        public bool viewAuth { get; set; }
        public bool createAuth { get; set; }
        public bool modifyAuth { get; set; }
        public bool deleteAuth { get; set; }
        public bool exportAuth { get; set; }
    }
}
