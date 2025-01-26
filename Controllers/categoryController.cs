using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommer_web_api.DTO;
using ecommer_web_api.Interfaces;
using ecommer_web_api.Model;
using ecommer_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommer_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]  
    public class categoryController:ControllerBase
    {
        // Defendency Injection
        private ICategoryService _categoryService;
        public categoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            /// direct defendency
            /// categoryService class er upr depend na kore upper class interface template er upr depend korteche
        }

        /// GET: /api/categories => read categories
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] string searchValue = "")
        {
            //  Console.WriteLine($"Search Value: {searchValue}");

            var categoryList = await _categoryService.GetAllCategories();

            if(!string.IsNullOrEmpty(searchValue))
            {
                var searchedCategories = categoryList.Where(c=> !string.IsNullOrEmpty(searchValue) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            // Console.WriteLine($"Found Categories: {searchedCategories.Count}");
                return Ok(searchedCategories);
            } 

            return Ok(categoryList);
        }

         /// GET: /api/categories/{categoryId} => read categories
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _categoryService.GetCategoryById(categoryId);

            if(foundCategory == null)
            {
                return NotFound("Category with this id does not exists");
            }

            return Ok(foundCategory);
        }

         /// POST: /api/categories => create a categories
        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromBody] CategoryCreateDto categoryData)
        {
            /// input validation
            if(string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category name is required and can not be empty");
            }

            var categoryReadDto = await _categoryService.CreateCategory(categoryData);

            return Created($"/api/categories/{categoryReadDto.CategoryId}", categoryReadDto);
        }

        /// PUT: /api/categories/{categoryId} => update a category
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            if(categoryData == null)
            {
                return BadRequest("Category data is missing");
            }
            
            var foundCategory = await _categoryService.UpdateCategoryById(categoryId,categoryData);
 
            if(foundCategory == null)
            {
                return NotFound("Category with this id does not exists");
            }

            return NoContent();
        }

        /// DELETE: /api/categories => delete a category by id
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _categoryService.DeleteCategoryById(categoryId);

            if(!foundCategory)
            {
                return NotFound("Category with this id does not exists");
            }
 
            return NoContent();
        }
     }
}