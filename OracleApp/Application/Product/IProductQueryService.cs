using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;
using System.Collections.Generic;

namespace OracleApp.Application.Product
{
    public interface IProductQueryService
    {
        List<ProductDal> Search();
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductSearcher _productSearcher;

        public ProductQueryService(IProductSearcher productSearcher)
        {
            _productSearcher = productSearcher;
        }

        public List<ProductDal> Search()
        {
            return _productSearcher.Search();
        }
    }
}
