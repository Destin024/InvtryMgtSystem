using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace InvtryMgtSystemAPI.Authentication
{
    public class Register
    {
        [Required(ErrorMessage ="User Name is Requirer")]
        public string  UserName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        public string   Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
    }
}
