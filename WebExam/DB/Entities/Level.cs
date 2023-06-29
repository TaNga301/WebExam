using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebExam.DB.Entities
{
    [Table("Level")]
    public class Level
    {
        [Key]
        [Required]
        public int Level_ID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [DisplayName("Level Name")]
        public string LevelName { get; set; }
    }
}