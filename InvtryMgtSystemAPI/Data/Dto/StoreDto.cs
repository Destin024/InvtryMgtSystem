using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreQuantity { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
