using OracleApp.Infrastructure.Persistence.Dal.Product;
using System.Collections.Generic;

namespace OracleApp.Infrastructure.Persistence.Searchers.Product
{
    public interface IProductSearcher
    {
        List<ProductDal> Search();
    }
}
