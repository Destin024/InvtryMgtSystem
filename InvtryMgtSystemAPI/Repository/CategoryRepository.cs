using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateCategoryAsync(Category category)
        {
          await  _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public  ICollection<Category>GetCategories()
        {
            return  _context.Categories.ToList();
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            return await _context.Categories.OrderBy(c => c.Id == id).FirstOrDefaultAsync();
        }

        public bool CategoryExists(Guid id)
        {
             return _context.Categories.Any(c => c.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(string name)
        {
            return await _context.Categories.Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        // public Category GetCategoryTrimToUpper(CategoryDto createCategory)
        // {
        //     return GetCategoriesAsync()
        //         .Where(c => c.Name.Trim().ToUpper() == createCategory.Name.TrimEnd().ToUpper()).FirstOrDefault();
        // }
    }
}
 