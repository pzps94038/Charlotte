using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class RefreshTokenLog
    {
        [Key]
        public string RefreshToken { get; set; }
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMain UserMain { get; set; }
        [Required]
        public DateTime CreateDate  { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
