using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ProductSearchResult> SearchAsync(ProductSearchCriteria criteria)
        {
            var select = Select(criteria);
            var from = From(criteria);
            var where = Where(criteria);
            var orderBy = OrderBy(criteria);

            var count = await GetCountAsync(select, from, where, orderBy, criteria);

            if (count != null && count.count != 0)
            {
                string sql = BuildPagedResult(select, from, where, orderBy, criteria);

                var list = await OracleContextAsync.QueryForListAsync<ProductDal>(sql);

                return new ProductSearchResult
                {
                    Items = list.ToList(),
                    Count = count.count
                };
            }
            return new ProductSearchResult
            {
                Items = new List<ProductDal>(),
                Count = 0
            };
        }

        public async Task<ProductDal> GetAsync(decimal productId)
        {
            var criteria = new ProductSearchCriteria
            {
                ProductId = Convert.ToInt32(productId)
            };

            string sql = BuildResult(Select(criteria), From(criteria), Where(criteria));

            return await OracleContextAsync.QueryForObjAsync<ProductDal>(sql);
        }

        public override async Task<CountDal> GetCountAsync(string select, string from, string where, string orderBy, ProductSearchCriteria criteria)
        {
            string sql = BuildCount(Select(criteria), From(criteria), Where(criteria), OrderBy(criteria));

            return await OracleContextAsync.QueryForObjAsync<CountDal>(sql);
        }

    }
}