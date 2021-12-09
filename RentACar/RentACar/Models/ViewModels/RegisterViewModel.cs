using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Voorletters")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Initials { get; set; }

        [DisplayName("Tussenvoegsels")]
        public string MiddleName { get; set; }

        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string LastName { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Dit is geen geldige email!")]
        public string Email { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Adress { get; set; }

        [DisplayName("Postcode")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string ZipCode { get; set; }

        [DisplayName("Woonplaats")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string City { get; set; }

        [DisplayName("Gebruikersnaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string UserName { get; set; }

        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten.", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen!")]
        public string PasswordConfirm { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string RoleName { get; set; }
    }
}
