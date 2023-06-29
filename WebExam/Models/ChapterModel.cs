using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebExam.DB.Entities;
using WebExam.Helpers.Validate;

namespace WebExam.Models
{
    public class ChapterModel
    {
        [Key]
        [Required]
        public int ChapterID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Chapter Name")]
        public string ChapterName { get; set; }
        [Required]
        [ChapterValidate]
        [DisplayName("Subject")]
        public int SubjectID { get; set; }
        [DisplayName("Subject")]
        public string SubjectName { get; set; }
        public List<SelectListItem> Subjects { get; set; }

        public ChapterModel()
        {
        }

        public ChapterModel(Chapter chapter)
        {
            ChapterID = chapter.ChapterID;
            ChapterName = chapter.ChapterName;
            SubjectID = chapter.SubjectID;
            SubjectName = string.Empty;
        }
    }
}