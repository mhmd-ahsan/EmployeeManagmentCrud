using Microsoft.AspNetCore.Identity;

namespace EmployeeManagment.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
