using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface IStoreRepository
    {
        ICollection<Store> GetStores();
        Store GetStore(int id);
        Store GetStore(string name);
        bool StoreExists(int id);
        bool CreateStore(Store store);
        bool UpdateStore(Store store);
        bool DeleteStore(Store store);
        bool save();
    }
}
