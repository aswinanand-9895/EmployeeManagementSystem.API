using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.API.Models.DTO
{
    public class UpdateEmployeeDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Employee name should be of atleast 4 characters")]
        public string empName { get; set; }
        [Required]
        public string empDesignation { get; set; }
        [Required]
        public string empLocation { get; set; }
    }
}
