using EmployeeManagment.Data;
using EmployeeManagment.Dtos;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EmployeeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employee = await dbContext.Employees.ToListAsync();

                if (!employee.Any())
                {
                    return NotFound(new { message = "No Employee Found" });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employees.", error = ex.Message });
            }
        }

        // Get Employee by Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeebyId(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(new { message = "No Employee Found" });
                }

                return Ok(new { message = "Employee Found", data = employee });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the employee.", error = ex.Message });
            }
        }

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                    Department = employeeDto.Department,
                    HireDate = employeeDto.HireDate
                };

                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployeebyId), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the employee.", error = ex.Message });
            }
        }

        // Update Employee
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(new { message = $"The Employee with ID {id} was not found." });
                }

                employee.Name = update.Name;
                employee.Email = update.Email;
                employee.Phone = update.Phone;
                employee.Department = update.Department;
                employee.HireDate = update.HireDate;

                dbContext.Employees.Update(employee);
                await dbContext.SaveChangesAsync();

                return Ok(new { message = "Employee Successfully Updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the employee.", error = ex.Message });
            }
        }

        // Delete Employee
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(new { message = $"No Employee found with ID: {id}" });
                }

                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();

                return Ok(new { message = $"Employee with ID {id} was successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the employee.", error = ex.Message });
            }
        }
    }
}
