using InvtryMgtSystemAPI.Models;
using Microsoft.VisualBasic;
using System;

namespace InvtryMgtSystemAPI.Data.Dto
{
    public class TransferDto
    {
        public Guid TransferId { get; set; }
        public int TransferQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public Guid  StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime CreatedAt{ get; set; }
    }
}
