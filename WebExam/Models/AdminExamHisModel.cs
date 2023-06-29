using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebExam.Models
{
    public class AdminExamHisModel
    {
        [DisplayName("Exam His ID")]
        public int ExamHisID { get; set; }
        [DisplayName("Exam ID")]
        public int ExamID { get; set; }
        [DisplayName("Student ID")]
        public int StudentID { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Take Exam Time")]
        public int TakeExamTime { get; set; }
        public Decimal Score { get; set; }
        public string Questions { get; set; }
        public string Answers { get; set; }
        [DisplayName("Exam Date")]
        public DateTime CreatedDate { get; set; }
    }
}