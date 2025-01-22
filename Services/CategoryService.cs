using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommer_web_api.Controllers;
using ecommer_web_api.DTO;
using ecommer_web_api.Model;

namespace ecommer_web_api.Services
{
    public class CategoryService
    {
        private static readonly List<Category> _categories = new List<Category>();

        public List<CategoryReadDto>GetAllCategories()
        {
            var categoryList = _categories.Select(c => new CategoryReadDto{
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return categoryList;
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if(foundCategory == null)
            {
                return null;
            }

            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            _categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt
            };

            return categoryReadDto;
        }

        public CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryId);
 
            if(foundCategory == null)
            {
                return null;
            }

            if(!string.IsNullOrEmpty(categoryData.Name))
            {
                foundCategory.Name = categoryData.Name;
            }
            
            if(!string.IsNullOrEmpty(categoryData.Description))
            {
                foundCategory.Description = categoryData.Description;
            }

            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public bool DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryId);
 
            if(foundCategory == null)
            {
                return false;
            }
            
            _categories.Remove(foundCategory);
            return true;
        }
    }
}