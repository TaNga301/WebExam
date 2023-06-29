using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebExam.Models
{
    public class LoginModel
    {
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        public string Password { get; set; }
    }
}