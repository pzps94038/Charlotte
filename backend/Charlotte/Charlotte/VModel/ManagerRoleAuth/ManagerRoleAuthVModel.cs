using Charlotte.Database.Model;

namespace Charlotte.VModel.ManagerRoleRouter
{
    public class ManagerRoleAuthVModel
    {
        public int roleId { get; set; }
        public int routerId { get; set; }
        public string routerName { get; set; }
        public bool viewAuth { get; set; }
        public bool createAuth { get; set; }
        public bool modifyAuth { get; set; }
        public bool deleteAuth { get; set; }
        public bool exportAuth { get; set; }
        public ManagerRoleAuthVModel() { }
        public ManagerRoleAuthVModel(ManagerRoleAuth authData, string routerName)
        {
            this.roleId = authData.RoleId;
            this.routerId = authData.RouterId;
            this.routerName = routerName;
            this.viewAuth = authData.ViewAuth == "Y";
            this.modifyAuth = authData.ModifyAuth == "Y";
            this.createAuth = authData.CreateAuth == "Y";
            this.deleteAuth = authData.DeleteAuth == "Y";
            this.exportAuth = authData.ExportAuth == "Y";
        }
        public ManagerRoleAuthVModel(int roleId, int routerId, string routerName)
        {
            this.roleId = roleId;
            this.routerId = routerId;
            this.routerName = routerName;
            this.viewAuth = false;
            this.createAuth = false;
            this.modifyAuth = false;
            this.deleteAuth = false;
            this.exportAuth = false;
        }
        public ManagerRoleAuthVModel(int roleId, int routerId, string routerName, bool viewAuth, bool createAuth, bool modifyAuth, bool deleteAuth, bool exportAuth)
        {
            this.roleId = roleId;
            this.routerId = routerId;
            this.routerName = routerName;
            this.viewAuth = viewAuth;
            this.createAuth = createAuth;
            this.modifyAuth = modifyAuth;
            this.deleteAuth = deleteAuth;
            this.exportAuth = exportAuth;
        }
    }

    public class CheckManagerRoleAuthVModel<T>
    {
        public T viewAuth { get; set; }
        public T createAuth { get; set; }
        public T modifyAuth { get; set; }
        public T deleteAuth { get; set; }
        public T exportAuth { get; set; }
    }
}
