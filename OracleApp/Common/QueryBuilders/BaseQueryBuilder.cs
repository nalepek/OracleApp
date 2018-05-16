using OracleApp.Common.Searchers;
using OracleApp.Infrastructure.Persistence.Dal;
using System;
using System.Threading.Tasks;

namespace OracleApp.Common.QueryBuilders
{
    public abstract class BaseQueryBuilder<TSearchCriteria>
        where TSearchCriteria : Criteria
    {
        public BaseQueryBuilder()
        {
        }

        public abstract Task<CountDal> GetCountAsync(string select, string from, string where, string orderBy, TSearchCriteria criteria);

        public string BuildPagedResult(string select, string from, string where, string orderBy, TSearchCriteria criteria)
        {
            var sql = String.Format(" {0} FROM ({1} {2}) {3} {4} {5}",
                                            select,
                                            String.Format(" {0}, row_number() OVER ({1}) rn ", select, orderBy),
                                            from,
                                            where,
                                            RowNumber(criteria),
                                            orderBy
                                            );

            return sql;
        }

        public string BuildResult(string select, string from, string where)
        {
            var sql = String.Format(" {0} {1} {2} ",
                                            select,
                                            from,
                                            where
                                            );
            return sql;
        }

        public string BuildCount(string select, string from, string where, string orderBy)
        {
            var sql = String.Format("SELECT count FROM (SELECT COUNT(1) AS COUNT FROM ({0} {1} {2} {3} ))", select, from, where, orderBy);

            return sql;
        }

        #region QueryParts

        public abstract string Select(TSearchCriteria criteria);
        public abstract string From(TSearchCriteria criteria);
        public abstract string Where(TSearchCriteria criteria);
        public abstract string OrderBy(TSearchCriteria criteria);

        public string RowNumber(TSearchCriteria criteria)
        {
            var fromRow = criteria.Page * (criteria.PageSize + 1);
            var toRow = criteria.PageSize * (criteria.Page + 1);

            var sql = String.Format(" AND rn BETWEEN {0} AND {1} ", fromRow, toRow);
            return sql;
        }

        #endregion QueryParts
    }
}
//SELECT 
//    product_id,
//    name,
//    description,
//    price, FROM (
//      SELECT
//          product_id,
//          name,
//          description,
//          price,
//          row_number() over (order by product_id asc) rn
//      FROM products
//    ) where 1 = 1
//  and rn between 7 and 12
//  order by product_id asc

//select count from(
//select count(1) as count from(
//SELECT
//product_id,
//name,
//description,
//price

//  FROM products
//where 1 = 1

//order by product_id asc))