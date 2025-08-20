using System.Linq;
using System.Net;
using System.Web.Http;
using Erp_sistemi1.Models;

namespace Erp_sistemi1.Controllers
{
    public class CategoriesApiController : ApiController
    {
        private UygulamaDbContext db = new UygulamaDbContext();

        // GET api/categoriesapi/5
        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // DELETE api/categoriesapi/5
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
                return NotFound();

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(new { message = "Kategori başarıyla silindi." });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
