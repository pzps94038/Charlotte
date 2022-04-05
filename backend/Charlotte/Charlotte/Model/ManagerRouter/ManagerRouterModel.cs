namespace Charlotte.Model.ManagerRouter
{
    public class ManagerRouterModel
    {
        public string routerName { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
        public int groupId { get; set; }
        public bool flag { get; set; }
    }

    public class ManagerRouterBatchDeleteModel
    {
        public string link { get; set; }
        public string icon { get; set; }
        public int groupId { get; set; }
        public bool flag { get; set; }
    }
}
