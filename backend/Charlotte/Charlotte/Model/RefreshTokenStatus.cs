using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.Model
{
    public class RefreshTokenStatus
    {
        [Key, Column(Order = 0)]
        public string RefreshToken { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public DateTime CreateDate  { get; set; }
    }
}
