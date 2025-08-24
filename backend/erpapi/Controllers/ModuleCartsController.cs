using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleCartsController : ControllerBase
    {
        private readonly IModuleCartManager _manager;
        public ModuleCartsController(IModuleCartManager manager) => _manager = manager;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _manager.GetAsync(id);
            return res is null ? NotFound() : Ok(res);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var res = await _manager.GetByUserAsync(userId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ModuleCartForCreateDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.CartID }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModuleCartForUpdateDto dto)
        {
            await _manager.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manager.DeleteAsync(id);
            return NoContent();
        }
    }
}
