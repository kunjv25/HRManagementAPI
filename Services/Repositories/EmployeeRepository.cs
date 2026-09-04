using HRManagementAPI.Data;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Repositories.Interfaces;
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

        // Get employees query
        public IQueryable<Employee> GetAll()
        {
            return _context.Employees.AsQueryable();
        }

        // Get employee by ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // Add employee
        public async Task CreateEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        // Update employee
        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);        
        }

        // Delete employee
        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        // Check if employee exists
        public async Task<bool> IsEmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        // Check if email already exists
        public async Task<bool> IsEmployeeEmailExistsAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email);
        }

        // Save changes to database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}