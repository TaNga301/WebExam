using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebExam.DB.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Required]
        public int RoleID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}