using System;
using System.Collections.Generic;
using System.Linq;
using OracleApp.Common.QueryBuilders;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Infrastructure.Persistence.QueryBuilders.Product
{
    public class ProductQueryBuilder : BaseQueryBuilder<ProductSearchCriteria>, IProductSearcher
    {
        public ProductQueryBuilder()
        {

        }

        public override string Select(ProductSearchCriteria criteria)
        {
            return @" SELECT product_id,
                             name,
                             description,
                             price ";
        }

        public override string From(ProductSearchCriteria criteria)
        {
            return " FROM Products ";
        }

        public override string Where(ProductSearchCriteria criteria)
        {
            return string.Concat(" WHERE 1=1 ",
                criteria.ProductId.HasValue ? " AND product_id = " + criteria.ProductId.Value : null );
        }

        private string OrderBy(ProductSearchCriteria criteria)
        {
            return string.Concat(" ORDER BY ",
                                    !string.IsNullOrWhiteSpace(criteria.Sort) ? String.Format(" {0} " , criteria.Sort) : " product_id ",
                                    !string.IsNullOrWhiteSpace(criteria.Order) ? String.Format(" {0} " , criteria.Order) : " asc ");                                    
        }

        public List<ProductDal> Search(ProductSearchCriteria criteria)
        {
            var select = Select(criteria);
            var from = From(criteria);
            var where = Where(criteria);
            var orderBy = OrderBy(criteria);

            string sql = BuildSql(select, from, where, orderBy, criteria);

            var list = OracleContext.QueryForList<ProductDal>(sql).ToList();

            return list;
        }

        public ProductDal Get(ProductSearchCriteria criteria)
        {
            string sql = BuildSql(Select(criteria), From(criteria), Where(criteria), OrderBy(criteria), criteria);

            var result = OracleContext.QueryForObj<ProductDal>(sql);

            return (ProductDal)result;
        }

    }
}

//SELECT* FROM(
//  SELECT
//    product_id,
//    name,
//    description,
//    price,
//    row_number() over (order by product_id asc) rn
//  FROM products
//  ) where 1 = 1
//  and rn between 7 and 12
//  order by product_id asc