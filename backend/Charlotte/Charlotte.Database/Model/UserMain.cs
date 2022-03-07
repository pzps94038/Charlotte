using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charlotte.DataBase.Model
{
    public class UserMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(20)]
        public string Account { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(250)]
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
