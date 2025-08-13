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

        // Var olan uçlar (liste + create)
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            _orderService.CreateOrder(order);
            return Ok(order);
        }

        // --- Yeni uçlar: Detay + Güncelle ---
        // /api/order/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<OrderDetailsDto>> GetDetails(int id)
        {
            var dto = await _orderService.GetOrderDetailsAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        // /api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetailsDto dto)
        {
            if (id != dto.OrderID) return BadRequest("Mismatched id");
            await _orderService.UpdateOrderAsync(dto);
            return NoContent();
        }
    }
}
