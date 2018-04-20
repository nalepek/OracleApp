using System.Collections.Generic;
using System.Linq;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Infrastructure.Persistence.QueryBuilders.Product
{
    public class ProductQueryBuilder : IProductSearcher
    {
        private string Select()
        {
            return @" SELECT product_id,
                             name,
                             description,
                             price ";
        }

        private string From()
        {
            return " FROM Products ";
        }

        private string Where(ProductSearchCriteria criteria)
        {
            return string.Concat(" WHERE 1=1 ",
                criteria.ProductId.HasValue ? " AND product_id = " + criteria.ProductId.Value : null );
        }

        private string OrderBy()
        {
            return "";
        }

        public List<ProductDal> Search(ProductSearchCriteria criteria)
        {
            string sql = string.Concat(Select(), From(), Where(criteria), OrderBy());
            var list = OracleContext.QueryForList<ProductDal>(sql).ToList();

            return list;
        }

        public ProductDal Get(ProductSearchCriteria criteria)
        {
            string sql = string.Concat(Select(), From(), Where(criteria), OrderBy());
            var result = OracleContext.QueryForObj<ProductDal>(sql);

            return (ProductDal)result;
        }
    }
}
