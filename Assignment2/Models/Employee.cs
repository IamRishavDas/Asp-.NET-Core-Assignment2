using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class Employee
    {
        [Key]
        [StringLength(30, MinimumLength = 1)]
        public string EmployeeId { get; set; }
        [StringLength(30)]
        [Required]
        public string EmployeeName { get; set; }
        [Range(21, 100)]
        [Required]
        public int EmployeeAge { get; set; }
        [Required]
        [Range(1000, double.MaxValue)]
        public decimal Salary { get; set; }
        [ForeignKey("DepartmentId")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }

        
    }
}
