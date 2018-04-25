using OracleApp.Common.Searchers;
using System;

namespace OracleApp.Common.QueryBuilders
{
    public abstract class BaseQueryBuilder<TSearchCriteria>
        where TSearchCriteria: Criteria
    {
        public BaseQueryBuilder()
        {
        }

        public string BuildSql(string select, string from, string where, string orderBy, TSearchCriteria criteria)
        {
            var finalSql = String.Format(" {0} FROM ( {1} {2} ) {3} {4} {5}", 
                                            select,
                                            String.Format(" {0}, row_number() OVER ( {1} ) rn ", select, orderBy),
                                            from,
                                            where,
                                            RowNumber(criteria),
                                            orderBy
                                            );

            return finalSql;
        }

        #region QueryParts

        public abstract string Select(TSearchCriteria criteria);
        public abstract string From(TSearchCriteria criteria);
        public abstract string Where(TSearchCriteria criteria);

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