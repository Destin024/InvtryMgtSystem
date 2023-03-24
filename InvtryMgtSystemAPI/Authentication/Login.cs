using System.ComponentModel.DataAnnotations;

namespace InvtryMgtSystemAPI.Authentication
{
    public class Login
    {
        [Required(ErrorMessage ="Username is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
    }
}
