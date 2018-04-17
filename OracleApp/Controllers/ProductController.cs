using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OracleApp.Application.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductQueryService _productQueryService;

        public ProductController(ProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ProductDal> Get()
        {
            string sql = "select product_id, name, description, price from products";
            var list = OracleContext.QueryForList<ProductDal>(sql).ToList();

            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
