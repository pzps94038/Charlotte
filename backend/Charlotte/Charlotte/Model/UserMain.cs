using System.ComponentModel.DataAnnotations;

namespace Charlotte.Model
{
    public class UserMain
    {
        [Key]
        [StringLength(20)]
        public string UserAccount { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
