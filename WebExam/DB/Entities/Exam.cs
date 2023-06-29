using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebExam.DB.Entities
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        [Required]
        [DisplayName("Exam ID")]
        public int ExamID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Exam Time")]
        public int ExamTime { get; set; }
        [Required]
        [DisplayName("Numb Of Questions")]
        public int NumOfQuestions { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Levels { get; set; }
        [Required]
        [DisplayName("Subject ID")]
        public int SubjectID { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }

        public Exam()
        {
            Status = true;
        }
    }
}