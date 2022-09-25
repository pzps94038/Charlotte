namespace Charlotte.VModel.Product
{
    public class ProductVModel
    {
        public int ProductId { get; set; } // 產品ID
        public string ProductName { get; set; } // 產品名稱
        public string Type { get; set; } // 產品類別
        public string ProductImgPath { get; set; } // 產品圖片
        public string ProductDescription { get; set; } // 產品說明
        public int Inventory { get; set; } // 存貨
        public int SellPrice { get; set; } // 售價
    }
}
