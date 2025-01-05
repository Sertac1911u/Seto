using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
