using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class LoginLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginLogID { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Flag { get; set; }

    }
}
