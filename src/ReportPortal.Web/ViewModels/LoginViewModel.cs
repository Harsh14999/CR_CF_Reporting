using System.ComponentModel.DataAnnotations;

namespace ReportPortal.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your Windows user ID.")]
        [Display(Name = "Windows user ID")]
        public string WindowsUserId { get; set; }

        [Display(Name = "Keep me signed in on this machine")]
        public bool KeepMeSignedIn { get; set; }
    }
}