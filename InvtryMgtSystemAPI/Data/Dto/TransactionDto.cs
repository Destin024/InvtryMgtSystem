using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int InventoryId { get; set; }
        public string  InventoryName { get; set; } 
        public int UserId { get; set; }
        public int UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
