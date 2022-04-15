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
    public class ProductImg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductImgId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductInformation ProductInformation { get; set; }
        public string ProductImgPath { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
