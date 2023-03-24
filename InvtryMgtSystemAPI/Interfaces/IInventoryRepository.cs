using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface IInventoryRepository
    {
        ICollection<Inventory> GetInventories();


        Inventory GetInventory(int inventoryId);
        bool InventoryExists(int inventoryId);
        bool CreateInventory(Inventory inventory);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(Inventory inventory);
        bool save();
        
    }
}
