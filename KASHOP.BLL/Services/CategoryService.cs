using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var categories = await _categoryrepository.GetAllAsync(new string[] {nameof(Category.Translations)});
            return categories.Adapt<List<CategoryResponse>>();//user model
             
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request) 
        {
          var category = request.Adapt<Category>();

           await _categoryrepository.CreateAsync(category);

            return category.Adapt<CategoryResponse>();
            
        }

        public async Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryrepository.GetOne(filter, new string[] { nameof(Category.Translations) });
            return category.Adapt<CategoryResponse>();
        }

        public async Task<bool> UpdateCategory(int id, CategoryRequest request)
        {
            var category = await _categoryrepository.GetOne(
        c => c.Id == id,
        new[] { nameof(Category.Translations) });

            if (category == null)
                return false;

            category.Translations = request.Translations
                .Adapt<List<CategoryTranslation>>();

            return await _categoryrepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _categoryrepository.GetOne(c => c.Id == id);
            if(category == null) return false;
            return await _categoryrepository.DeleteAsync(category);
        }

       
    }
}
