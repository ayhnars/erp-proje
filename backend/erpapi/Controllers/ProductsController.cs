using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Dtos.ProductDtos;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/products")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductsController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productManager.GetAllAsync();
            var productDtos = GetProductDtos(products);
            return Ok(productDtos);
        }

        private IEnumerable<ProductDto> GetProductDtos(IEnumerable<Product> products)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<ProductDto>(product);
            return Ok(dto);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDtoForInsert dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);
            await _productManager.CreateAsync(product);

            return Ok(new { message = "Ürün başarıyla eklendi." });
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDtoForUpdate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.ProductID)
                return BadRequest(new { message = "ID uyuşmuyor." });

            var product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(dto, product);
            await _productManager.UpdateAsync(product);

            return Ok(new { message = "Ürün başarıyla güncellendi." });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productManager.DeleteAsync(product);
            return Ok(new { message = "Ürün başarıyla silindi." });
        }
    }
}
