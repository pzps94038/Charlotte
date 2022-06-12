namespace Charlotte.Model.ManagerProduct
{
    public class ManagerPorductModel
    {
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public int Inventory { get; set; }
        public int CostPrice { get; set; }
        public int SellPrice { get; set; }
        public int FactoryId { get; set; }
        public string ProductImgPath { get; set; }
    }
}
