using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebExam.DB.Entities
{
    [Table("ExamHis")]
    public class ExamHis
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Exam His ID")]
        public int ExamHisID { get; set; }
        [Required]
        [DisplayName("Exam ID")]
        public int ExamID { get; set; }
        [DisplayName("Student ID")]
        public int StudentID { get; set; }
        [Required]
        [DisplayName("Take Exam Time")]
        public int TakeExamTime { get; set; }
        [Required]
        public Decimal Score { get; set; }
        public string Questions { get; set; }
        public string Answers { get; set; }
        [Required]
        [DisplayName("Exam Date")]
        public DateTime CreatedDate { get; set; }
    }
}