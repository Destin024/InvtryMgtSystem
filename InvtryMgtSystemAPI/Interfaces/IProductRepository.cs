using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
   public interface IProductRepository
    {
        ICollection<ProductInventory> GetProducts();
        ProductInventory GetProduct(Guid id);
        bool ProductExists(Guid id);

        bool CreateProduct(ProductInventory product);
        bool UpdateProduct(ProductInventory product);
        bool DeleteProduct(ProductInventory product);
        bool save();

    }
}
