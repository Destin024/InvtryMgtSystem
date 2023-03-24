using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Repository
{
    public class StoreRepository:IStoreRepository
    {
        private readonly DataInvntryContext _context;

        public StoreRepository(DataInvntryContext context)
        {
            _context = context;
        }

        public bool CreateStore(Store store)
        {
            _context.Add(store);
            return save();
        }

        public bool DeleteStore(Store store)
        {
            _context.Remove(store);
            return save();
        }

        public Store GetStore(int id)
        {
            return _context.Stores.Where(s => s.Id == id).FirstOrDefault();
        }

        public Store GetStore(string name)
        {
            return _context.Stores.Where(s => s.Name == name).FirstOrDefault();
        }

        public ICollection<Store> GetStores()
        {
            return _context.Stores.ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StoreExists(int id)
        {
            return _context.Stores.Any(s => s.Id == id);
        }

        public bool UpdateStore(Store store)
        {
            _context.Update(store);
            return save();
        }
    }
}
