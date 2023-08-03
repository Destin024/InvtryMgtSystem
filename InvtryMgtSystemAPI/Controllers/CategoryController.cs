using AutoMapper;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public  IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("categoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            // if (!await _categoryRepository.CategoryExistsAsync(categoryId))
            // {
            //     return NotFound();
            // }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryAsync(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto createCategory)
        {
            if (createCategory == null)
            {
                return BadRequest(ModelState);
            }
            // var category = _categoryRepository.get

            // if (category != null)
            // {
            //     ModelState.AddModelError("", "Category Already Exists");
            //     return StatusCode(400, ModelState);
            // }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category categoryMap =new()
            {
                Id = Guid.NewGuid(),
                Name = createCategory.Name,
                CreatedAt = createCategory.CreatedAt
            };

            await _categoryRepository.CreateCategoryAsync(categoryMap);

            if (categoryMap ==null)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response{Status = StatusCodes.Status500InternalServerError,Message="Something went wrong while saving"});
            }
            return Ok(new Models.Response{Status = StatusCodes.Status201Created,Message = "Category Created Successfully"});

        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async  Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody]CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest(ModelState);
            }
            if (categoryId == null)
            {
                return BadRequest(ModelState);
            }
            var categoryToUpdate = await _categoryRepository.GetCategoryAsync(categoryId);
            if(categoryToUpdate == null) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new Models.Response{Status = StatusCodes.Status404NotFound, Message = "Category not found"});
            }
            // if (!_categoryRepository.CategoryExistsAsync(categoryId))
            // {
            //     return NotFound();
            // }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             categoryToUpdate.Name = updatedCategory.Name;
             categoryToUpdate.CreatedAt = updatedCategory.CreatedAt;

            if (categoryToUpdate !=null)
            {
                ModelState.AddModelError("", "Something went wrong while updating category");
                return StatusCode(500, ModelState);
            }
            await _categoryRepository.UpdateCategoryAsync(categoryToUpdate);
            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            // if (!_categoryRepository.CategoryExists(categoryId))
            // {
            //     return NotFound();
            // }
            var categoryToDelete = await _categoryRepository.GetCategoryAsync(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (categoryToDelete!=null)
            {
                ModelState.AddModelError("", "Something went wrong while deleting Category");
                return StatusCode(400, ModelState);
            }
            await _categoryRepository.DeleteCategoryAsync(categoryToDelete);

            return NoContent();
        }
    }
}
