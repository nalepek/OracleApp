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
        public async Task<ProductSearchResult> SearchProducts([FromQuery] ProductSearchCriteria criteria)
        {
            return await _productQueryService.SearchAsync(criteria);
        }

        // GET api/values/5
        [HttpPost("get")]
        public async Task<ProductDal> GetProduct([FromBody] int productId)
        {
            return await _productQueryService.GetAsync(productId);
        }

        [HttpPost("update")]
        public async Task<ProductDal> UpdateProduct([FromBody] ProductDal product)
        {
            return await _productCommandService.UpdateAsync(product);
        }

        [HttpPost("delete")]
        public async Task DeleteProduct([FromBody] decimal productId)
        {
            await _productCommandService.DeleteAsync(productId);
        }

        [HttpPost("add")]
        public async Task AddProduct([FromBody] ProductDal product)
        {
            await _productCommandService.AddAsync(product);
        }

    }
}
