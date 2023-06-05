using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Repository
{
    public class CategoryRepository :ICategoryRepository
    {
        private readonly DataInvntryContext _context;
        public CategoryRepository(DataInvntryContext context)
        {
            _context = context;
        }

        public bool CreateCategory(Category category)
        {
          var saved=  _context.Update(category);
            return save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(Guid id)
        {
            return _context.Categories.OrderBy(c => c.Id == id).FirstOrDefault();
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return save();
        }

        public Category GetCategory(string name)
        {
            return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
 