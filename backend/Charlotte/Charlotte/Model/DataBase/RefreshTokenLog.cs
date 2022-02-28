using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.Model.DataBase
{
    public class RefreshTokenLog
    {
        [Key, Column(Order = 0)]
        public string RefreshToken { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public DateTime CreateDate  { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
