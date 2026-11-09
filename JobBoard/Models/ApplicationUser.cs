using Microsoft.AspNetCore.Identity;

namespace JobBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Job>? Jobs { get; set; }           
        public virtual ICollection<Application>? Applications { get; set; } 
    }
}

