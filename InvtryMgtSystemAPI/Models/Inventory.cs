using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int InventoryQuantity { get; set; }
        public Store Store { get; set; }
        public int  StoreId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
