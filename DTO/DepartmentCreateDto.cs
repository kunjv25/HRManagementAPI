using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}