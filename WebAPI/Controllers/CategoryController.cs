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
            var result = await _categoryService.AddCategoryAsyncByLanguage(model);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCategoryDTO model)
        {
            model.Id = Guid.Parse(id);
            await _categoryService.UpdateCategoryAsyncByLanguage(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var langCode = Request.Headers.AcceptLanguage;
            var result = _categoryService.GetCategoryById(id, "az");
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.DeleteCategory(id);
            if(result.Success)
                return Ok(result);    
            return NotFound(result);
        }
    }
}
