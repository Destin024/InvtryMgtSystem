using System;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class PaymentModeDto
    {
        public int Id { get; set; }
        public string Momo { get; set; }
        public string Airtel { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
