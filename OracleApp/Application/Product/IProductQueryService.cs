using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Application.Product
{
    public interface IProductQueryService
    {
        ProductSearchResult Search(ProductSearchCriteria criteria);
        ProductDal Get(int productId);
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductSearcher _productSearcher;

        public ProductQueryService(IProductSearcher productSearcher)
        {
            _productSearcher = productSearcher;
        }

        public ProductDal Get(int productId)
        {
            return _productSearcher.Get(productId);
        }

        public ProductSearchResult Search(ProductSearchCriteria criteria)
        {
            return _productSearcher.Search(criteria);
        }
    }
}
