using System;
using System.Linq;
using System.Net;

using Erp_sistemi1.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace Erp_sistemi1.Controllers.Api
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public ProductsController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // GET: api/products
        [HttpGet]
        [Route("")]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        [Route("")]
        public IActionResult CreateProduct(Product product)
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
        public IActionResult UpdateProduct(int id, Product product)
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
