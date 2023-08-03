using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class ProductInventoryDto
    {
        public Guid ProductInventoryId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public int ProductPrice { get; set; }
        public Guid  CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}