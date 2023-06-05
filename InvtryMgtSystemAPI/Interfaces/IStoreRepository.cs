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
        Store GetStore(Guid id);
        Store GetStore(string name);
        bool StoreExists(Guid id);
        bool CreateStore(Store store);
        bool UpdateStore(Store store);
        bool DeleteStore(Store store);
        bool save();
    }
}
