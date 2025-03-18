using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.Models.Domain;
using EmployeeManagementSystem.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Repositories
{
    public class EmployeeRepository : IRepository
    {
        private readonly EmployeeDataDbContext dbContext;

        public EmployeeRepository(EmployeeDataDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await this.dbContext.employees.AddAsync(employee);
            await this.dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteEmployeeByIdAsync(int id)
        {
            var fetchedEmployeeDomainModel = await this.dbContext.employees.FirstOrDefaultAsync((e)=>e.empId == id);
            if (fetchedEmployeeDomainModel == null)
            {
                return null;
            }
            else
            {
                this.dbContext.Remove(fetchedEmployeeDomainModel);
                await this.dbContext.SaveChangesAsync();
                return fetchedEmployeeDomainModel;
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int? pageNumber = 1, int? pageSize = 4)
        {
            var employees = this.dbContext.employees.AsQueryable();
            //FILTERING.........
            // Apply the filter if provided
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {

                if (filterOn.Equals("empLocation", StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure we handle null values properly and apply Contains
                    employees = employees.Where(employee => employee.empLocation != null &&
                                                             employee.empLocation.Contains(filterQuery));
                }
            }

            //SORTING.....
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("empName",StringComparison.OrdinalIgnoreCase))
                {
                    employees = (bool)isAscending ? employees.OrderBy(employee=>employee.empName):employees.OrderByDescending(employee=>employee.empName);
                }
                if(sortBy.Equals("empLocation",StringComparison.OrdinalIgnoreCase))
                {
                    employees = (bool)isAscending ? employees.OrderBy(employee => employee.empLocation) : employees.OrderByDescending(employee => employee.empLocation);
                }
            }
            //PAGINATION......
            var skipResults = (pageNumber - 1) * pageSize;

            return await employees.Skip((int)skipResults).Take((int)pageSize).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await dbContext.employees.FirstOrDefaultAsync((e) => e.empId == id);
        }

        public async Task<Employee?> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            var employeeDomainModel = await this.dbContext.employees.FirstOrDefaultAsync(e => e.empId == id);
            if (employeeDomainModel == null) {
                return null;
            }
            else
            {
                employeeDomainModel.empName = employeeDto.empName;
                employeeDomainModel.empLocation = employeeDto.empLocation;
                employeeDomainModel.empDesignation = employeeDto.empDesignation;

                this.dbContext.SaveChangesAsync();
                return employeeDomainModel;
            }

        }
    }
}
