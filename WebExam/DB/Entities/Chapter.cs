using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.Helpers.Validate;

namespace WebExam.DB.Entities
{
    [Table("Chapter")]
    public class Chapter
    {
        [Key]
        [Required]
        public int ChapterID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Chapter Name")]
        public string ChapterName { get; set; }
        [Required]
        [ChapterValidate]
        [DisplayName("Subject ID")]
        public int SubjectID { get; set; }
    }
}