using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryDTO model)
        {
            await _categoryService.AddCategoryAsyncByLanguage(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCategoryDTO model)
        {
            model.Id = Guid.Parse(id);
            await _categoryService.UpdateCategoryAsyncByLanguage(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id, string langCode)
        {
            var result = _categoryService.GetCategoryById(id, langCode);
            return Ok(result);
        }
    }
}
