using HRManagementAPI.DTO.Employee;
using HRManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        /***
         * get all employees
         * -------------------
         * GET: api/employees
         */
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetEmployees(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            if (pageNumber < 1)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Page number must be greater than 0."
                });
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Page size must be between 1 and 100."
                });
            }

            var employees = await _employeeService.GetAllAsync(pageNumber, pageSize, search);

            return Ok(employees);
        }


        /***
         * get 1 employee
         * ------------------------
         * GET: api/employees/5 
         * */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        /***
         * create employee
         * --------------------
         * POST: api/employees 
         */ 
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = employee.Id },
                employee
            );
        }


        /***
         * Update employee
         * ---------------------
         * PUT: api/employees/5 
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
        {
            var employee = await _employeeService.UpdateAsync(id, dto);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        /***
         * Delete employee
         * -----------------------
         * DELETE: api/employees/5 
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Employee not found."
                });
            }

            return NoContent();
        }
    }
}