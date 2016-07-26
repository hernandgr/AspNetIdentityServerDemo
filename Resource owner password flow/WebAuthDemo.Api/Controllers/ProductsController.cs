using System.Collections.Generic;
using System.Web.Http;
using WebAuthDemo.Api.Models;

namespace WebAuthDemo.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [Authorize]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "T-shirt", Price = 100},
                new Product {Id = 2, Name = "Cap", Price = 30}
            };

            return Ok(products);
        }
    }
}