using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Application.Product
{
    public interface IProductQueryService
    {
        ProductSearchResult Search(ProductSearchCriteria criteria);
        ProductDal Get(ProductSearchCriteria criteria);
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductSearcher _productSearcher;

        public ProductQueryService(IProductSearcher productSearcher)
        {
            _productSearcher = productSearcher;
        }

        public ProductDal Get(ProductSearchCriteria criteria)
        {
            return _productSearcher.Get(criteria);
        }

        public ProductSearchResult Search(ProductSearchCriteria criteria)
        {
            return _productSearcher.Search(criteria);
        }
    }
}
