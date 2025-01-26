using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ecommer_web_api.Controllers;
using ecommer_web_api.data;
using ecommer_web_api.DTO;
using ecommer_web_api.Interfaces;
using ecommer_web_api.Model;
using Microsoft.EntityFrameworkCore;

namespace ecommer_web_api.Services
{
    public class CategoryService:ICategoryService
    {
        // private static readonly List<Category> _categories = new List<Category>();

        private readonly AppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryReadDto>> GetAllCategories()
        {
            // var categoryList = _categories.Select(c => new CategoryReadDto{
            //     CategoryId = c.CategoryId,
            //     Name = c.Name,
            //     Description = c.Description,
            //     CreatedAt = c.CreatedAt
            // }).ToList();

            // return categoryList;

            /// after mapping  
    
            var categories = await _appDbContext.Categories.ToListAsync();

            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            // if(foundCategory == null)
            // {
            //     return null;
            // }

            // return new CategoryReadDto
            // {
            //     CategoryId = foundCategory.CategoryId,
            //     Name = foundCategory.Name,
            //     Description = foundCategory.Description,
            //     CreatedAt = foundCategory.CreatedAt
            // };

            /// after mapper
            return foundCategory == null ? null :_mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            // var newCategory = new Category
            // {
            //     CategoryId = Guid.NewGuid(),
            //     Name = categoryData.Name,
            //     Description = categoryData.Description,
            //     CreatedAt = DateTime.UtcNow
            // };

            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            await _appDbContext.Categories.AddAsync(newCategory);
            await _appDbContext.SaveChangesAsync();

            // var categoryReadDto = new CategoryReadDto
            // {
            //     CategoryId = newCategory.CategoryId,
            //     Name = newCategory.Name,
            //     Description = newCategory.Description,
            //     CreatedAt = newCategory.CreatedAt
            // };

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
 
            if(foundCategory == null)
            {
                return null;
            }

            // if(!string.IsNullOrEmpty(categoryData.Name))
            // {
            //     foundCategory.Name = categoryData.Name;
            // }
            
            // if(!string.IsNullOrEmpty(categoryData.Description))
            // {
            //     foundCategory.Description = categoryData.Description;
            // }

            _mapper.Map(categoryData,foundCategory);

            await _appDbContext.SaveChangesAsync();

            _appDbContext.Categories.Update(foundCategory);

            // return new CategoryReadDto
            // {
            //     CategoryId = foundCategory.CategoryId,
            //     Name = foundCategory.Name,
            //     Description = foundCategory.Description,
            //     CreatedAt = foundCategory.CreatedAt
            // };

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
 
            if(foundCategory == null)
            {
                return false;
            }
            
            _appDbContext.Categories.Remove(foundCategory);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}