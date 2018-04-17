using System.Collections.Generic;
using System.Linq;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Infrastructure.Persistence.QueryBuilders.Product
{
    public class ProductQueryBuilder : IProductSearcher
    {

        public string Select()
        {
            string sql = "select product_id, name, description, price from products";
            var list = OracleContext.QueryForList<ProductDal>(sql).ToList();

            //return list;

            return "";
        }

        public string From()
        {
            return "";
        }

        public string Where()
        {
            return "";
        }

        public string OrderBy()
        {
            return "";
        }

        public List<ProductDal> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}
