using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public Inventory Inventory { get; set; }
        public Guid InventoryId { get; set; }
        //public User User { get; set; }
        //public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
