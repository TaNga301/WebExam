using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.DB.Entities;

namespace WebExam.Models
{
    public class AnswerModel
    {
        public int AnswerID { get; set; }
        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar")]
        public string AnswerTitle { get; set; }
        public int QuestionID { get; set; }
        [Required]
        [DisplayName("Answer Content")]
        public string AnswerContent { get; set; }
        [Required]
        [DisplayName("Is Correct Answer")]
        public bool IsCorrect { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public bool NewFg { get; set; }
        [Required]
        public bool DelFg { get; set; }

        public AnswerModel()
        {
            Status = true;
            IsCorrect = false;
            NewFg = true;
            DelFg = false;
        }

        public AnswerModel(Answer answer)
        {
            //AnswerID = answer.AnswerID;
            QuestionID = answer.QuestionID;
            AnswerContent = answer.AnswerContent;
            Status = true;
            IsCorrect = false;
            NewFg = true;
            DelFg = false;
        }
    }
}