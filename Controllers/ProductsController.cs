using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Erp_sistemi1.Data;
using Erp_sistemi1.Models;

namespace Erp_sistemi1.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly UygulamaDbContext _context;

        public ProductsController()
        {
            _context = new UygulamaDbContext();
        }

        // GET: api/products
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            product.CreatedAt = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + product.ProductID), product);
        }

        // PUT: api/products/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productInDb = _context.Products.Find(id);
            if (productInDb == null)
                return NotFound();

            productInDb.CompanyID = product.CompanyID;
            productInDb.CategoryID = product.CategoryID;
            productInDb.ProductName = product.ProductName;
            productInDb.ProductCode = product.ProductCode;
            productInDb.Quantity = product.Quantity;
            productInDb.UnitType = product.UnitType;
            productInDb.MinStockLevel = product.MinStockLevel;
            productInDb.SellPrice = product.SellPrice;
            productInDb.BuyPrice = product.BuyPrice;
            productInDb.ProductDescription = product.ProductDescription;
            productInDb.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/products/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _context.Products.Find(id);
            if (productInDb == null)
                return NotFound();

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
