using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebExam.DB.Entities;

namespace WebExam.Models
{
    public class ExamModel
    {
        [Required]
        [DisplayName("ID")]
        public int ExamID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        [DisplayName("Tên đề")]
        public string Title { get; set; }
        [Required]
        [DisplayName("thời gian làm bài")]
        public int ExamTime { get; set; }
        [Required]
        [DisplayName("số câu")]
        public int NumOfQuestions { get; set; }
        public List<LevelModel> Levels { get; set; }
        [Required]
        [DisplayName("Chủ đề")]
        public int SubjectID { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Ngày chỉnh sửa")]
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public string DisplayMessage { get; set; }
        [DisplayName("Chủ đề")]
        public string SubjectNm { get; set; }
        public List<Comment> Comments { get; set; }

        public ExamModel()
        {
            Status = true;
            Levels = new List<LevelModel>();
        }

        public ExamModel(Exam exam)
        {
            ExamID = exam.ExamID;
            Title = exam.Title;
            ExamTime = exam.ExamTime;
            NumOfQuestions =  exam.NumOfQuestions;
            Levels = new List<LevelModel>();
            SubjectID = exam.SubjectID;
            CreatedDate = exam.CreatedDate;
            SubjectNm = String.Empty;
            Status = exam.Status;
            ModifiedDate = exam.ModifiedDate;
        }
    }
}