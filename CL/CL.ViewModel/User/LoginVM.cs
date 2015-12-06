using System.ComponentModel.DataAnnotations;

namespace CL.ViewModel.User
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public string IsRemember { get; set; }
    }
}
