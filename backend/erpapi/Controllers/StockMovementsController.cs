using AutoMapper;
using Entities.Dtos.StockMovementDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/stockmovements")]
   // [Authorize] // İstersen rol bazlı ekleyebilirsin
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementManager _stockMovementManager;
        private readonly IMapper _mapper;

        public StockMovementsController(IStockMovementManager stockMovementManager, IMapper mapper)
        {
            _stockMovementManager = stockMovementManager;
            _mapper = mapper;
        }

        // GET: api/StockMovements
        [HttpGet]
        public async Task<IActionResult> GetStockMovements()
        {
            var movements = await _stockMovementManager.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<StockMovementDto>>(movements);
            return Ok(dtos);
        }

        // GET: api/StockMovements/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockMovement(int id)
        {
            var movement = await _stockMovementManager.GetByIdAsync(id);
            if (movement == null)
                return NotFound();

            var dto = _mapper.Map<StockMovementDto>(movement);
            return Ok(dto);
        }

        // POST: api/StockMovements
        [HttpPost]
        public async Task<IActionResult> CreateStockMovement([FromBody] StockMovementDtoForInsert dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movement = _mapper.Map<StockMovement>(dto);
            await _stockMovementManager.CreateAsync(movement);

            return Ok(new { message = "Stok hareketi başarıyla eklendi." });
        }

        // PUT: api/StockMovements/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStockMovement(int id, [FromBody] StockMovementDtoForUpdate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.MovementID)
                return BadRequest(new { message = "ID uyuşmuyor." });

            var movement = await _stockMovementManager.GetByIdAsync(id);
            if (movement == null)
                return NotFound();

            _mapper.Map(dto, movement);
            await _stockMovementManager.UpdateAsync(movement);

            return Ok(new { message = "Stok hareketi başarıyla güncellendi." });
        }

        // DELETE: api/StockMovements/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStockMovement(int id)
        {
            var movement = await _stockMovementManager.GetByIdAsync(id);
            if (movement == null)
                return NotFound();

            await _stockMovementManager.DeleteAsync(movement);
            return Ok(new { message = "Stok hareketi başarıyla silindi." });
        }
    }
}
