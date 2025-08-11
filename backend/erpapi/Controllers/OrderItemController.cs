using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace erpapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemManager _manager;

        public OrderItemController(IOrderItemManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _manager.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _manager.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem item)
        {
            await _manager.CreateAsync(item);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderItem item)
        {
            await _manager.UpdateAsync(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manager.DeleteAsync(id);
            return Ok();
        }
    }
}
