using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using Services;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        
        [HttpPost]
        public IActionResult Create(Order order)
        {
            _orderService.CreateOrder(order);
            return Ok(order);
        }
    }
}
