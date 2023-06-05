using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Models
{
    public class Response
    {
        [Required]
        public int Status { get; set; }
        [Required]
        public string Message { get; set; }
    }
}