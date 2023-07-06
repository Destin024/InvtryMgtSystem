using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Task<Category> GetCategoryAsync(Guid id);
        Task<Category> GetCategoryAsync(string name);
        //Category GetCategoryTrimToUpper(CategoryDto createCategory);
         bool CategoryExists(Guid id);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task SaveAsync();
    }
}
