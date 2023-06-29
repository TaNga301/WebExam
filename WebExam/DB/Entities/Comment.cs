using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebExam.DB.Entities
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public int ExamID { get; set; }
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}