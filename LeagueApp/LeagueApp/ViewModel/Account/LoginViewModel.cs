using System.ComponentModel.DataAnnotations;

namespace LeagueApp.ViewModel.Account
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
