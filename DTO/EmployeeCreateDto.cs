using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO.Employee
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; } = string.Empty;


        [Range(1, double.MaxValue, ErrorMessage = "Salary must be Positive value")]

        public decimal Salary { get; set; }


        [Required(ErrorMessage = "Joining date is required.")]
        public DateTime JoiningDate { get; set; }


        public bool IsActive { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        public int DepartmentId { get; set; }
    }
}