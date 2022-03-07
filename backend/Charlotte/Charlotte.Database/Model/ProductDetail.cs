using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class ProductDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
        public string? ProductImgPath { get; set; }
        public int? Inventory { get; set; }
        [Required]
        public int SellPrice { get; set; }
        [Required]
        public int CostPrice { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
