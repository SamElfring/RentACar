using Microsoft.AspNetCore.Identity;

namespace RentACar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Initials { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }
    }
}
