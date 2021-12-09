using System.ComponentModel.DataAnnotations;

namespace RentACar.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
        
        [Display(Name = "Onthoud mij")]
        public bool RememberMe { get; set; }
    }
}
