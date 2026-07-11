using KASHOP.BLL.Services;
using KASHOP.DAL.Data;
using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryservice;

        public CategoriesController(IStringLocalizer<SharedResources> localizer, ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryservice = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryservice.GetAllCategoriesAsync();

            return Ok(new { _localizer["Success"].Value, categories });
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {

            var response = await _categoryservice.CreateCategoryAsync(request);

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var category = await _categoryservice.GetCategory(cat =>  cat.Id == id);

            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var deleted = await _categoryservice.DeleteCategory(id);
            if (!deleted) return BadRequest();

            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id , CategoryRequest request)
        {

            var updated = await _categoryservice.UpdateCategory(id, request);
            if (!updated) return BadRequest();

            return Ok();
        }
    }
}
