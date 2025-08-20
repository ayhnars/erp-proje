using Erp_sistemi1.Models;
using Erp_sistemi1.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Erp_sistemi1.Controllers.Api
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly UygulamaDbContext _context = new UygulamaDbContext();

        // GET api/customers
        [HttpGet, Route("")]
        public IHttpActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost, Route("")]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Content(HttpStatusCode.Created, customer);
        }

        // PUT api/customers/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            customer.CustomerCode = updatedCustomer.CustomerCode;
            customer.CustomerName = updatedCustomer.CustomerName;
            customer.Phone = updatedCustomer.Phone;
            customer.Email = updatedCustomer.Email;
            customer.Address = updatedCustomer.Address;
            customer.CompanyID = updatedCustomer.CompanyID;

            _context.SaveChanges();

            return Ok(customer);
        }

        // DELETE api/customers/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
