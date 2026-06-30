using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryrepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryrepository = categoryRepository;
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryrepository.GetAllAsync();
            return categories.Adapt<List<CategoryResponse>>();//user model
             
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request) 
        {
          var category = request.Adapt<Category>();

           await _categoryrepository.CreateAsync(category);

            return category.Adapt<CategoryResponse>();
            
        }
    }
}
