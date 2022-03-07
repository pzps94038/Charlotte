using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTypeId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
