using HRManagementAPI.Data;
using HRManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRDbContext _context;

        public DepartmentRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> IsDepartmentExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> IsDepartmentNameExistsAsync(string name)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentName == name);
        }

        public async Task<bool> IsDepartmentNameExistsAsync(string name, int id)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentName == name && d.Id != id);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}