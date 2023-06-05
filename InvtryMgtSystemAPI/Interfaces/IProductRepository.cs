using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
   public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(Guid id);
        bool ProductExists(Guid id);

        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool save();

    }
}
