using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Dtos
{
    public class CreateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Department { get; set; }

        public DateTime HireDate { get; set; }
    }
}
