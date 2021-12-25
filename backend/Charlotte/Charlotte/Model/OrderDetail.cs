using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.Model
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductDetail ProductDetail { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public int Amount { get; set; }
        public int Total { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
