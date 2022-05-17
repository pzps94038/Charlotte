using Charlotte.Database.Model;

namespace Charlotte.VModel.ManagerRoleRouter
{
    public class ManagerRoleAuthVModel
    {
        public int RoleId { get; set; }
        public int RouterId { get; set; }
        public string RouterName { get; set; }
        public bool ViewAuth { get; set; }
        public bool CreateAuth { get; set; }
        public bool ModifyAuth { get; set; }
        public bool DeleteAuth { get; set; }
        public bool ExportAuth { get; set; }
        public ManagerRoleAuthVModel() { }
        public ManagerRoleAuthVModel(ManagerRoleAuth authData, string routerName)
        {
            this.RoleId = authData.RoleId;
            this.RouterId = authData.RouterId;
            this.RouterName = routerName;
            this.ViewAuth = authData.ViewAuth == "Y";
            this.ModifyAuth = authData.ModifyAuth == "Y";
            this.CreateAuth = authData.CreateAuth == "Y";
            this.DeleteAuth = authData.DeleteAuth == "Y";
            this.ExportAuth = authData.ExportAuth == "Y";
        }
        public ManagerRoleAuthVModel(int roleId, int routerId, string routerName)
        {
            this.RoleId = roleId;
            this.RouterId = routerId;
            this.RouterName = routerName;
            this.ViewAuth = false;
            this.CreateAuth = false;
            this.ModifyAuth = false;
            this.DeleteAuth = false;
            this.ExportAuth = false;
        }
        public ManagerRoleAuthVModel(int roleId, int routerId, string routerName, bool viewAuth, bool createAuth, bool modifyAuth, bool deleteAuth, bool exportAuth)
        {
            this.RoleId = roleId;
            this.RouterId = routerId;
            this.RouterName = routerName;
            this.ViewAuth = viewAuth;
            this.CreateAuth = createAuth;
            this.ModifyAuth = modifyAuth;
            this.DeleteAuth = deleteAuth;
            this.ExportAuth = exportAuth;
        }
    }

    public class CheckManagerRoleAuthVModel<T>
    {
        public T ViewAuth { get; set; }
        public T CreateAuth { get; set; }
        public T ModifyAuth { get; set; }
        public T DeleteAuth { get; set; }
        public T ExportAuth { get; set; }
    }
}
