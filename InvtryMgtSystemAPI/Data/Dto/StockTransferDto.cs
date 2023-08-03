using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvtryMgtSystemAPI.Models;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class StockTransferDto
    {
        public Guid StockTransferId { get; set; }
        public Guid ProductInventoryId { get; set; }
        public int TransferQuantity { get; set; }
        //public int Quantity { get; set; }
        // public  Store Store { get; set; }
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime  CreatedDate { get; set; }
    }
}