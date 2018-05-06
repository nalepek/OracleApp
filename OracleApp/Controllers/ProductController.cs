using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IProductCommandService _productCommandService;

        public ProductController(IProductQueryService productQueryService, IProductCommandService productCommandService)
        {
            _productQueryService = productQueryService;
            _productCommandService = productCommandService;
        }

        // GET api/values
        [HttpGet("search")]
        public ProductSearchResult SearchProducts([FromQuery] ProductSearchCriteria criteria)
        {
            return _productQueryService.Search(criteria);
        }

        // GET api/values/5
        [HttpPost("Get")]
        public ProductDal GetProduct([FromBody] ProductSearchCriteria criteria)
        {
            return _productQueryService.Get(criteria);
        }

        [HttpPost("Update")]
        public Task<ProductDal> UpdateProduct([FromBody] int productId)
        {
            return _productCommandService.Update(productId);
        }

        [HttpPost("Delete")]
        public void DeleteProduct([FromBody] int productId)
        {
            _productCommandService.Delete(productId);
        }
    }
}
