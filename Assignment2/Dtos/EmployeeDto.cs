using Assignment2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Dtos
{
    public class EmployeeDto
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
        [Required]
        public string DepartmentId { get; set; }

        public override string ToString()
        {
            return $"Employee(Id: {this.EmployeeId}, Name: {this.EmployeeName}, Age: {this.EmployeeAge}, Salary: {this.Salary}, DepartmentId: {this.DepartmentId})";
        }
    }
}
