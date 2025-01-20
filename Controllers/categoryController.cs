using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommer_web_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace ecommer_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class categoryController:ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        /// GET: /api/categories => read categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            //  Console.WriteLine($"Search Value: {searchValue}");
            if(!string.IsNullOrEmpty(searchValue))
            {
                var searchedCategories = categories.Where(c=> !string.IsNullOrEmpty(searchValue) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            // Console.WriteLine($"Found Categories: {searchedCategories.Count}");
                return Ok(searchedCategories);
            }

            return Ok(categories);
        }

        /// POST: /api/categories => create a categories
        [HttpPost]
        public IActionResult CreateCategories([FromBody] Category categoryData)
        {
            /// input validation
            if(string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category name is required and can not be empty");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(newCategory);

            return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
        }

        /// DELETE: /api/categories => delete a category by id
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if(foundCategory == null)
            {
                return NotFound("Category with this id does not exists");
            }

            categories.Remove(foundCategory);
            return NoContent();
        }

        /// PUT: /api/categories/{categoryId} => update a category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoriesId, [FromBody] Category categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoriesId);
 
            if(foundCategory == null)
            {
                return NotFound("Category with this id does not exists");
            }

            if(categoryData == null)
            {
                return BadRequest("Category data is missing");
            }

            if(!string.IsNullOrEmpty(categoryData.Name))
            {
                foundCategory.Name = categoryData.Name;
            }
            
            if(!string.IsNullOrEmpty(categoryData.Description))
            {
                foundCategory.Description = categoryData.Description;
            }

            return NoContent();
        }
    }
}