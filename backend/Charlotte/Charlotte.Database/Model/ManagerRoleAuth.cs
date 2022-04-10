using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Database.Model
{
    public class ManagerRoleAuth
    {
        [Key]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ManagerRole ManagerRole { get; set; }
        [Key]
        [Required]
        public int RouterId { get; set; }
        [ForeignKey("RouterId")]
        public virtual Router Router { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string ViewAuth { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string CreateAuth { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string ModifyAuth { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string DeleteAuth { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string ExportAuth { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        
        public DateTime? ModifyDate { get; set; }
    }
}
