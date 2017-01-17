using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crowe.Data.Infrastructure;
using Crowe.Domain;
using System.Web.Http.Description;
using Crowe.Services;
using System.Web.ModelBinding;
using System.Data.Entity.Infrastructure;

namespace CroweWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        // GET: api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }
            _productService.UpdateProduct(product);

            try
            {
                _productService.SaveProduct();
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

        // POST: api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productService.CreateProduct(product);

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5 
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(product);

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _productService.GetProduct(id) != null;
        }
    }
}

