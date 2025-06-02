using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Dtos
{
    public class RegisterDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
