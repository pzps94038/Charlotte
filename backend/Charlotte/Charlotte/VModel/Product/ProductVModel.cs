namespace Charlotte.VModel.Product
{
    public class ProductVModel
    {
        public int ProductId { get; set; } // 產品ID
        public string ProductName { get; set; } // 產品名稱
        public string ProductType { get; set; } // 產品類別
        public int Inventory { get; set; } // 存貨
        public int SellPrice { get; set; } // 售價
    }
}
