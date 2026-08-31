using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO.Employee
{
    public class EmployeeUpdateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}