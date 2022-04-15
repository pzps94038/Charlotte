using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductInformation ProductInformation { get; set; }
        [Required]
        public int ProductAmount { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
