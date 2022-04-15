using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Database.Model
{
    public class Factory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FactoryId { get; set; }
        [Required]
        public string FactoryName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
