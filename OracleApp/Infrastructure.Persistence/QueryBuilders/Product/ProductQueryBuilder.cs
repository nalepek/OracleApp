using System;
using System.Collections.Generic;
using System.Linq;
using OracleApp.Common.QueryBuilders;
using OracleApp.Infrastructure.Persistence.Dal;
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

        public override string OrderBy(ProductSearchCriteria criteria)
        {
            return string.Concat(" ORDER BY ",
                                    string.IsNullOrWhiteSpace(criteria.Sort) || criteria.Sort == "undefined" ? " product_id " : String.Format(" {0} ", criteria.Sort),
                                    string.IsNullOrWhiteSpace(criteria.Order) ? " asc " : String.Format(" {0} " , criteria.Order));                                    
        }

        public ProductSearchResult Search(ProductSearchCriteria criteria)
        {
            var select = Select(criteria);
            var from = From(criteria);
            var where = Where(criteria);
            var orderBy = OrderBy(criteria);

            var count = GetCount(select, from, where, orderBy, criteria);

            if (count != null && count.count != 0)
            {
                string sql = BuildPagedResult(select, from, where, orderBy, criteria);

                var list = OracleContext.QueryForList<ProductDal>(sql).ToList();

                return new ProductSearchResult
                {
                    Items = list,
                    Count = count.count
                };
            }
            return new ProductSearchResult
            {
                Items = new List<ProductDal>(),
                Count = 0
            };
        }

        public ProductDal Get(decimal productId)
        {
            var criteria = new ProductSearchCriteria
            {
                ProductId = Convert.ToInt32(productId)
            };

            string sql = BuildResult(Select(criteria), From(criteria), Where(criteria));

            return OracleContext.QueryForObj<ProductDal>(sql);
        }

        public override CountDal GetCount(string select, string from, string where, string orderBy, ProductSearchCriteria criteria)
        {
            string sql = BuildCount(Select(criteria), From(criteria), Where(criteria), OrderBy(criteria));

            return OracleContext.QueryForObj<CountDal>(sql);
        }

        public ProductDal GetAsync(decimal productId)
        {
            throw new NotImplementedException();
        }
    }
}