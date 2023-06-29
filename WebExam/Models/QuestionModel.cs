using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebExam.DB.Entities;
using WebExam.Helpers.Validate;

namespace WebExam.Models
{
    public class QuestionModel
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
        [DisplayName("Correct Awnser")]
        public string CorrectAnswer { get; set; }
        [DisplayName("Level")]
        [Required]
        [QuestionValidate]
        public int Level_ID { get; set; }
        [DisplayName("Subject")]
        [Required]
        [QuestionValidate]
        public int SubjectID { get; set; }
        [DisplayName("Subject Name")]
        [QuestionValidate]
        public string SubjectName { get; set; }
        [DisplayName("Chapter")]
        [QuestionValidate]
        public int ChapterID { get; set; }
        [DisplayName("Chapter Name")]
        [QuestionValidate]
        public string ChapterName { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
        public bool Status { get; set; }
        [Required]
        public List<AnswerModel> Answers { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public List<SelectListItem> Chapters { get; set; }
        public List<SelectListItem> Levels { get; set; }
        public string DisplayMessage { get; set; }
        [DisplayName("Level")]
        public string Level { get; set; }
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [DisplayName("Chapter")]
        public string Chapter { get; set; }

        public QuestionModel() { Answers = new List<AnswerModel>(); }

        public QuestionModel(Question question)
        {
            QuestionID = question.QuestionID;
            QuestionContent = question.QuestionContent;
            CorrectAnswer = question.CorrectAnswer;
            Level_ID = question.Level_ID;
            SubjectID = question.SubjectID;
            ChapterID = question.ChapterID;
            CreatedDate = question.CreatedDate;
            Answers = new List<AnswerModel>();
            Status = question.Status;
        }

    }
}