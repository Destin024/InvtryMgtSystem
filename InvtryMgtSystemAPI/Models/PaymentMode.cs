using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Models
{
    public class PaymentMode
    {
        public int Id { get; set; }
        public string Momo { get; set; }
        public string Airtel { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
