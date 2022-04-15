using Charlotte.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    [Index(nameof(ProductTypeId))]
    public class ProductInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }

        [Required]
        public int Inventory { get; set; }
        [Required]
        public int CostPrice { get; set; }
        [Required]
        public int SellPrice { get; set; }

        [Required]
        public int FactoryId { get; set; }
        [ForeignKey("FactoryId")]
        public virtual Factory Factory { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [Required]
        public string ProductImgPath { get; set; }
    }
}
