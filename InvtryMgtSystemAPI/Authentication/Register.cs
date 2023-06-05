using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace InvtryMgtSystemAPI.Authentication
{
    public class Register
    {
        [Required(ErrorMessage ="User Name is Required")]
        public string  UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email Required") ]
        public string   Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
    }
}
