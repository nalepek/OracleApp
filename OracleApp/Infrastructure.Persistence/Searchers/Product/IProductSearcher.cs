using System.Threading.Tasks;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Searchers.Product
{
    public interface IProductSearcher
    {
        Task<ProductSearchResult> SearchAsync(ProductSearchCriteria criteria);

        //ProductDal Get(decimal productId);
        Task<ProductDal> GetAsync(decimal productId);
    }
}
