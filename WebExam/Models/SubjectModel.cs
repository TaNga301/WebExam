using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebExam.DB.Entities;
using WebExam.Helpers.Validate;

namespace WebExam.Models
{
    public class SubjectModel
    {
        [Key]
        [Required]
        [DisplayName("Subject ID")]
        public int SubjectID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }
        [Required]
        [SubjectValidate]
        [DisplayName("Employee")]
        public string EmployeeName { get; set; }
        [Required]
        [SubjectValidate]
        public int EmployeeID { get; set; }

        public List<SelectListItem> Employees { get; set; }

        public bool IsCanEdit { get; set; }

        public SubjectModel()
        {
        }

        public SubjectModel(Subject subject)
        {
            SubjectID = subject.SubjectID;
            SubjectName = subject.SubjectName;
            EmployeeID = subject.EmployeeID;
            EmployeeName = string.Empty;
            Employees = new List<SelectListItem>();
            IsCanEdit = false;
        }
    }
}