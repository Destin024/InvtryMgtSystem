using Microsoft.VisualBasic;
using System;

namespace InvtryMgtSystemAPI.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public  Store Store { get; set; }
        public int StoreId { get; set; }
        public DateTime  CreatedAt { get; set; }
    }
}
