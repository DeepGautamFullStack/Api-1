using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DeepCart.Models;
using System.Web;
using System.IO;
using DeepCart.ViewModel;

namespace DeepCart.Controllers
{
    public class ProductsController : ApiController
    {
        private DeepCartContext db = new DeepCartContext();


        // GET: api/Products
        [AllowAnonymous]
        public IEnumerable<ProductCategory> GetAllProductsByCategory()
        {
            var allProduct = db.Products.ToList();
            IEnumerable<ProductCategory> allProductByCategory = from p in allProduct
                                             group p by p.Category into sl
                                     select new ProductCategory()
                                     {
                                         Category = sl.Key,
                                         Products = sl.Select(x => new Product
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             Description = x.Description,
                                             Quantity = x.Quantity,
                                             SellerName = x.SellerName,
                                             ImageName = x.ImageName,
                                             ImageFile = Utility.ImageToByteArray(x.ImageName),
                                             UnitPrice = x.UnitPrice,
                                             Category = x.Category,
                                         }).ToList()
                                     };

            return allProductByCategory;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [HttpPost]
        public IHttpActionResult PostProduct()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Save Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmdd") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);

            //Save to DB
            Product product = new Product();
            product.Name= httpRequest["Name"];
            product.Description = httpRequest["Description"];
            product.Quantity = Convert.ToInt32( httpRequest["Quantity"]);
            product.UnitPrice = Convert.ToInt32(httpRequest["UnitPrice"]);
            product.SellerId = Convert.ToInt32(httpRequest["SellerId"]);
            product.SellerName = httpRequest["SellerName"];
            product.Category = httpRequest["Category"];
            product.TC = httpRequest["TC"];
            product.ImageName = imageName;

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}