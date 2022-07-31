using Charlotte.Model.ManagerOrderDetail;

namespace Charlotte.Model.ManagerOrder
{
    public class ManagerOrderModel
    {
        public int UserId { get; set; }
        public List<ManagerOrderDetailModel> OrderDetail { get; set; }
    }
}
