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
    public class EmployeeModel
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        [DisplayName("Role")]
        [Required]
        [EmployeeValidate]
        public int RoleID { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public List<SelectListItem> Roles { get; set; }
        [DisplayName("Role")]
        public string RoleName { get; set; }
        public string DisplayMessage { get; set; }
        public EmployeeModel()
        {

        }

        public EmployeeModel(Employee employee)
        {
            EmployeeID = employee.EmployeeID;
            FullName = employee.FullName;
            UserName = employee.UserName;
            Password = employee.Password;
            BirthDay = employee.BirthDay;
            Email = employee.Email;
            Phone = employee.Phone;
            Address = employee.Address;
            RoleID = employee.RoleID;
            CreatedDate = employee.CreatedDate;
            Status = employee.Status;
            Roles = new List<SelectListItem>();
            RoleName = String.Empty;
        }
    }
}