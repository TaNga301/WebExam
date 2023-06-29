using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.Helpers.Validate;

namespace WebExam.DB.Entities
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        [Required]
        [DisplayName("Subject ID")]
        public int SubjectID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }
        [Required]
        [SubjectValidate]
        [DisplayName("Employee ID")]
        public int EmployeeID { get; set; }
    }
}