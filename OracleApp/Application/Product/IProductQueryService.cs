using System.Threading.Tasks;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Application.Product
{
    public interface IProductQueryService
    {
        Task<ProductSearchResult> SearchAsync(ProductSearchCriteria criteria);
        Task<ProductDal> GetAsync(int productId);
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductSearcher _productSearcher;

        public ProductQueryService(IProductSearcher productSearcher)
        {
            _productSearcher = productSearcher;
        }

        public async Task<ProductDal> GetAsync(int productId)
        {
            return await _productSearcher.GetAsync(productId);
        }

        public async Task<ProductSearchResult> SearchAsync(ProductSearchCriteria criteria)
        {
            return await _productSearcher.SearchAsync(criteria);
        }
    }
}
