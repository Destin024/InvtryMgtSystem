using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Repository
{
    public class InventoryRepository:IInventoryRepository
    {
        private readonly DataInvntryContext _context;

        public InventoryRepository(DataInvntryContext context )
        {
            _context = context;
        }

        public bool CreateInventory(Inventory inventory)
        {
            _context.Add(inventory);
            return Save();
        }

        public bool DeleteInventory(Inventory inventory)
        {
            _context.Remove(inventory);
            return Save();
        }

        public ICollection<Inventory> GetInventories()
        {
            return _context.Inventories.OrderBy(i => i.InventoryId).ToList();
        }

        public Inventory GetInventory(Guid inventoryId)
        {
            return _context.Inventories.Where(i => i.InventoryId == inventoryId).FirstOrDefault();
        }


        public bool InventoryExists(Guid inventoryId)
        {
            return _context.Inventories.Any(i => i.InventoryId == inventoryId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInventory(Inventory inventory)
        {
            _context.Update(inventory);
            return Save();
        }
    }
}
