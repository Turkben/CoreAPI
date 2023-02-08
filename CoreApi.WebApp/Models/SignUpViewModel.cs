using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreApi.WebApp.Models
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string RePassword { get; set; }
    }
}
