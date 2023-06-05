using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class InventoryDto
    {

        public Guid InventoryId { get; set; }
        public int InventoryQuantity { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
