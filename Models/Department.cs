using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; } 
            = new List<Employee>();
    }
}