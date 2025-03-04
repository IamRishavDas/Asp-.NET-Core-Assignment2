using System.ComponentModel.DataAnnotations;

namespace Assignment2.Dtos
{
    public class DepartmentDto
    {
        [Key]
        [StringLength(30, MinimumLength = 2)]
        public string DepartmentId { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string DepartmentName { get; set; }

        public override string ToString()
        {
            return $"Department( Name: {this.DepartmentName} )";
        }
    }
}
