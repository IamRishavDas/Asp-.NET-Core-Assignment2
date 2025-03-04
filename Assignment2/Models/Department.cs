using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Department
    {
        [Key]
        [StringLength(30, MinimumLength = 2)]
        public string DepartmentId { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string DepartmentName { get; set; }
    }
}
