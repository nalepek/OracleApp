using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OracleApp.Application.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductQueryService _productQueryService;

        public ProductController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        // GET api/values
        [HttpPost("Search")]
        public IEnumerable<ProductDal> SearchProducts([FromBody] ProductSearchCriteria criteria)
        {
            return _productQueryService.Search(criteria);
        }

        // GET api/values/5
        [HttpPost("Get")]
        public ProductDal GetProduct([FromBody] ProductSearchCriteria criteria)
        {
            return _productQueryService.Get(criteria);
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
