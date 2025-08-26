using AutoMapper;
using Entities.Dtos.CustomerDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace erpapi.Controllers
{
    [ApiController]
    [Route("api/customers")]
   // [Authorize] // Gerekirse kaldırılabilir veya rol bazlı eklenebilir
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerManager customerManager, IMapper mapper)
        {
            _customerManager = customerManager;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerManager.GetAllAsync();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        // GET: api/Customers/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerManager.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            var dto = _mapper.Map<CustomerDto>(customer);
            return Ok(dto);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDtoForInsert dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(dto);
            await _customerManager.CreateAsync(customer);

            return Ok(new { message = "Müşteri başarıyla eklendi." });
        }

        // PUT: api/Customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDtoForUpdate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.CustomerID)
                return BadRequest(new { message = "ID uyuşmuyor." });

            var customer = await _customerManager.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            _mapper.Map(dto, customer);
            await _customerManager.UpdateAsync(customer);

            return Ok(new { message = "Müşteri başarıyla güncellendi." });
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerManager.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerManager.DeleteAsync(customer);
            return Ok(new { message = "Müşteri başarıyla silindi." });
        }
    }
}
