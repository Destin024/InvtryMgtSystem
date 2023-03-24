using System.ComponentModel.DataAnnotations;

namespace InvtryMgtSystemAPI.Authentication
{
    public class Response
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string Message  { get; set; }
    }
}
