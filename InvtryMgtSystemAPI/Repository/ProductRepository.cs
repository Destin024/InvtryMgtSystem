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

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return save();
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return save();
        }

        public Product GetProduct(Guid id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }


        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool ProductExists(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return save();
        }
    }
}
