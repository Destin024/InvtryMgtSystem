using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreQuantity { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
