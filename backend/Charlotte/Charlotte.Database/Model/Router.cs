using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Database.Model
{
    public class Router
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouterId { get; set; }

        [Required]
        [StringLength(250)]
        public string RouterName { get; set; }
        [Required]
        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(250)]
        public string Icon { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [StringLength(1)]
        [Column(TypeName = "nchar(1)")]
        public string Flag { get; set; }
    }
}
