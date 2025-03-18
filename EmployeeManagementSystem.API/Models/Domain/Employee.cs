using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.API.Models.Domain
{
    public class Employee
    {
        [Column("empid")]
        [Key]
        public int empId { get; set; }
        [Column("empName")]
        public string empName { get; set; }
        [Column("designation")]
        public string empDesignation { get; set; }
        [Column("empLocation")]
        public  string empLocation { get; set; }

    }
}
