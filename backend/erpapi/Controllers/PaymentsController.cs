using Entities.Dtos.PaymentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Hazırsa açabilirsin
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentManager _svc;

        public PaymentsController(IPaymentManager svc)
        {
            _svc = svc;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentForCreateDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PaymentID }, created);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _svc.GetAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpGet("by-cart/{cartId:int}")]
        public async Task<IActionResult> GetByCart(int cartId)
        {
            var list = await _svc.GetByCartAsync(cartId);
            return Ok(list);
        }

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] PaymentForUpdateDto dto)
        {
            await _svc.UpdateStatusAsync(id, dto.PaymentStatus);
            return NoContent();
        }
    }
}
