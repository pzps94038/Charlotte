using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class ManagerRefreshTokenLog
    {
        [Key]
        public string RefreshToken { get; set; }
        [Key]
        public int ManagerUserId { get; set; }
        [ForeignKey("ManagerUserId")]
        public virtual ManagerMain ManagerMain { get; set; }
        [Required]
        public DateTime CreateDate  { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
