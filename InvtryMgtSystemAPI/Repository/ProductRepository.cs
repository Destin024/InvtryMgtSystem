using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Repository
{
    public class ProductRepository :IProductRepository
    {
        private readonly DataInvntryContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataInvntryContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateProduct(ProductInventory product)
        {
            _context.Add(product);
            return save();
        }

        public bool DeleteProduct(ProductInventory product)
        {
            _context.Remove(product);
            return save();
        }

        public ProductInventory GetProduct(Guid id)
        {
            return _context.ProductInventories.Where(p => p.ProductInventoryId == id).FirstOrDefault();
        }


        public ICollection<ProductInventory> GetProducts()
        {
            return _context.ProductInventories.ToList();
        }

        public bool ProductExists(Guid id)
        {
            return _context.ProductInventories.Any(p => p.ProductInventoryId == id);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(ProductInventory product)
        {
            _context.Update(product);
            return save();
        }
    }
}
