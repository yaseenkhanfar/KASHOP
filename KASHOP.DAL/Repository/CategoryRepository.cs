using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _context.Categories.Include(c => c.Translations).ToListAsync();//join , domain model
            return categories;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            _context.AddAsync(category);
            _context.SaveChangesAsync();
            return category;
        }
    }
}
