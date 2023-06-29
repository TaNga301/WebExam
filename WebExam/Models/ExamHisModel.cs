using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebExam.Models
{
    public class ExamHisModel
    {
        [Key]
        [DisplayName("Exam His ID")]
        public int ExamHisID { get; set; }
        [DisplayName("Exam Title")]
        public string ExamTitle { get; set; }
        [DisplayName("Num Of Questions")]
        public int NumOfQuestions { get; set; }
        [DisplayName("Take Exam Time")]
        public string TakeExamTime { get; set; }
        public string Score { get; set; }

        [DisplayName("Exam Date")]
        public DateTime CreatedDate { get; set; }
    }
}