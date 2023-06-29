using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebExam.Helpers.Validate;

namespace WebExam.DB.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Required]
        [DisplayName("Employee ID")]
        public int EmployeeID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        public DateTime? BirthDay { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        [EmployeeValidate]
        public string Phone { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Address { get; set; }
        [DisplayName("Role ID")]
        [Required]
        [EmployeeValidate]
        public int RoleID { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public Employee()
        {
            Status = true;
        }
    }
}