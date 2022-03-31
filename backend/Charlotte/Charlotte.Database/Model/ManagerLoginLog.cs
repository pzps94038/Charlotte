using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class ManagerLoginLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginLogId { get; set; }
        [Required]
        public int ManagerUserId { get; set; }
        [ForeignKey("ManagerUserId")]
        public virtual ManagerMain ManagerMain { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Flag { get; set; }

    }
}
