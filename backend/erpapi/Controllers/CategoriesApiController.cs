using AutoMapper;
using Entities.Dtos.CategoryDtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/categoriesapi")] // küçük harfli manuel route kullanmak güvenli
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoriesApiController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryManager.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDtoForInsert dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(dto);
            await _categoryManager.CreateAsync(category);

            return Ok(new { message = "Kategori başarıyla eklendi." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDtoForUpdate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _mapper.Map(dto, category);
            await _categoryManager.UpdateAsync(category);

            return Ok(new { message = "Kategori başarıyla güncellendi." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryManager.DeleteAsync(category);
            return Ok(new { message = "Kategori başarıyla silindi." });
        }
    }
}
