using Charlotte.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    [Index(nameof(Account))]
    public class ManagerMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerUserId { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string Account { get; set; }
        
        [StringLength(250)]
        [Required]
        public string Password { get; set; }
        [StringLength(250)]
        [Required]
        public string Email { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }
        [StringLength(1)]
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Flag { get; set; }
        
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ManagerRole ManagerRole { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
