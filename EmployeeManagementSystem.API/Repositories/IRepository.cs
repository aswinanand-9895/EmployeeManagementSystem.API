using EmployeeManagementSystem.API.Models.Domain;
using EmployeeManagementSystem.API.Models.DTO;

namespace EmployeeManagementSystem.API.Repositories
{
    public interface IRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync(string? filterOn=null,string? filterQuery=null,string? sortBy=null,bool? isAscending=true,int? pageNumber=1,int? pageSize=4);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto);

        Task<Employee?> DeleteEmployeeByIdAsync(int id);

    }
}
