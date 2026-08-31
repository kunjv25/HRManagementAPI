using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO.Department
{
    public class DepartmentUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}