using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;
using System.Collections.Generic;

namespace OracleApp.Application.Product
{
    public interface IProductQueryService
    {
        List<ProductDal> Search(ProductSearchCriteria criteria);
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

        public List<ProductDal> Search(ProductSearchCriteria criteria)
        {
            return _productSearcher.Search(criteria);
        }
    }
}
