using AutoMapper;
using EmployeeManagementSystem.API.Models.Domain;
using EmployeeManagementSystem.API.Models.DTO;
using EmployeeManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public EmployeesController(IRepository employeeRepository,IMapper mapper)
        {
            this.repository = employeeRepository;
            this.mapper = mapper;
        }
        
        //GET - ALL Employeess
        [HttpGet]
        public async Task<IActionResult> getAllEmployees([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int? pageNumber=1, [FromQuery] int? pageSize=4)
        {
            //Fetch list of employee domain models from database
            var employees = await this.repository.GetAllEmployeesAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);
            //Map the domain models to dtos
            return Ok(mapper.Map<List<EmployeeDto>>(employees));
            return Ok(mapper.Map<List<EmployeeDto>>(employees));
        }

        //GET - Employee By ID
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> getEmployeeById([FromRoute] int id)
        {
            var employee = await this.repository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<EmployeeDto>(employee));
            }
        }

        //POST  - Add Employee
        [HttpPost]
        public async Task<IActionResult> addEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                //Map Employee Dto to Employee Domain Model
                var employeeDomainModel = mapper.Map<Employee>(employeeDto);
                var createdEmployee = await this.repository.AddEmployeeAsync(employeeDomainModel);
                //Map employee domain model to dto
                var createdEmployeeDto = mapper.Map<EmployeeDto>(createdEmployee);

                return CreatedAtAction(nameof(getEmployeeById), new { id = createdEmployeeDto.empId }, createdEmployeeDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
          

        }

        // PUT- Update Details of an Employee

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> updateEmployee([FromRoute] int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if(ModelState.IsValid)
            {
                //Fetch the employee details based on employee id
                var fetchedEmployeeDomainModel = await this.repository.UpdateEmployeeAsync(id, updateEmployeeDto);
                if (fetchedEmployeeDomainModel == null)
                {
                    return NotFound();
                }
                else
                {



                    return Ok(mapper.Map<EmployeeDto>(fetchedEmployeeDomainModel));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
         
        }

        //DELETE Delete an employee
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteEmployee([FromRoute] int id)
        {
            {
                var deletedEmployeeDomainModel = await this.repository.DeleteEmployeeByIdAsync(id);
                if (deletedEmployeeDomainModel == null)
                {
                    return NotFound();
                }
                else
                {
                   
                    return Ok(mapper.Map<EmployeeDto>(deletedEmployeeDomainModel));
                }
            }

        }
    }
}
