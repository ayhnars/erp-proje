using Microsoft.AspNetCore.Mvc;
using Services.Contrats;
using Entities.Models;
using Entities.Dtos;
using System.Threading.Tasks;

namespace erpapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Listele
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // Oluştur
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (order is null) return BadRequest("Order body boş.");
            _orderService.CreateOrder(order);

            // Eğer detay endpoint’in varsa CreatedAtAction güzel olur;
            // yoksa sadece Ok(order) dönebilirsin.
            return CreatedAtAction(nameof(GetDetails), new { id = order.OrderID }, order);
        }

        // Detay (serviste varsa)
        [HttpGet("{id}/details")]
        public async Task<ActionResult<OrderDetailsDto>> GetDetails(int id)
        {
            var dto = await _orderService.GetOrderDetailsAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        // Güncelle (serviste varsa)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetailsDto dto)
        {
            if (dto is null || id != dto.OrderID) return BadRequest("Id eşleşmiyor.");
            await _orderService.UpdateOrderAsync(dto);
            return NoContent();
        }
    }
}
