using HRManagementAPI.Data;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;

        public EmployeeRepository(HRDbContext context)
        {
            _context = context;
        }


        // Get all employees
        public Task<IQueryable<Employee>> GetAllAsync()
        {
            return Task.FromResult(
                _context.Employees.AsQueryable()
            );
        }


        // Get employee by ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        // Add employee
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }


        // Update employee
        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }


        // Delete employee
        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }


        // Check employee exists
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Employees
                .AnyAsync(e => e.Id == id);
        }


        // Check email already exists
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email);
        }
    }
}