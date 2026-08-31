using HRManagementAPI.DTO.Department;
using HRManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        /***
         * get all departments
         * ----------------------
         * GET: api/departments
         */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            return Ok(departments);
        }


        /***
         * get 1 department
         * -------------------------
         * GET: api/departments/5
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }


        /***
         * create department
         * -----------------------
         * POST: api/departments
         */
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDto dto)
        {
            var department = await _departmentService.CreateDepartmentAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = department.Id },
                department
            );
        }


        /***
         * Update department
         * ------------------------
         * PUT: api/departments/5
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentUpdateDto dto)
        {
            var department = await _departmentService.UpdateDepartmentAsync(id, dto);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }


        /***
         * Delete department
         * --------------------------
         * DELETE: api/departments/5
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _departmentService.DeleteDepartmentAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}