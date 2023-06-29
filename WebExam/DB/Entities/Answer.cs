using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.Models;

namespace WebExam.DB.Entities
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        [Required]
        public int AnswerID { get; set; }
        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar")]
        public string AnswerTitle { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Answer Content")]
        public string AnswerContent { get; set; }
        [Required]
        public bool Status { get; set; }

        public Answer()
        {
            Status = true;
        }

        public Answer(AnswerModel answer)
        {
            AnswerID = answer.AnswerID;
            AnswerTitle = answer.AnswerTitle;
            AnswerContent = answer.AnswerContent;
            QuestionID = answer.QuestionID;
            Status = true;
        }
    }
}