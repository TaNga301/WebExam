using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.Helpers.Validate;

namespace WebExam.DB.Entities
{
    [Table("Question")]
    public class Question
    {
        [Key]
        [Required]
        [DisplayName("Question ID")]
        public int QuestionID { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [DisplayName("Question Content")]
        public string QuestionContent { get; set; }
        [Column(TypeName = "nvarchar")]
        [DisplayName("Correct Answer")]
        [Required]
        public string CorrectAnswer { get; set; }
        [DisplayName("Level ID")]
        [Required]
        [QuestionValidate]
        public int Level_ID { get; set; }
        [DisplayName("Subject ID")]
        [Required]
        [QuestionValidate]
        public int SubjectID { get; set; }
        [DisplayName("Chapter ID")]
        [Required]
        [QuestionValidate]
        public int ChapterID { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public Question()
        {
            Status = true;
        }
    }
}