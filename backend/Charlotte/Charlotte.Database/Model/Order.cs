using Charlotte.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Database.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
