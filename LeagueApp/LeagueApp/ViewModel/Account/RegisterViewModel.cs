using System.ComponentModel.DataAnnotations;

namespace LeagueApp.ViewModel.Account
{
    public class RegisterViewModel
    {
        public string UserName { get; set; } = string.Empty;


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

    }
}
