using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Searchers.Product
{
    public interface IProductSearcher
    {
        ProductSearchResult Search(ProductSearchCriteria criteria);

        ProductDal Get(int productId);
    }
}
