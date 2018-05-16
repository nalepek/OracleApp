using System;
using System.Threading.Tasks;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        public async Task AddAsync(ProductDal newItem)
        {
            var sql = string.Format(@" INSERT INTO products(product_id, name, description, price) VALUES ({0}, '{1}', '{2}', {3})", 
                                        newItem.product_id, 
                                        newItem.name, 
                                        newItem.description, 
                                        newItem.price);

            await OracleContextAsync.CreateUpdateDeleteAsync(sql);
        }

        public async Task DeleteAsync(decimal id)
        {
            var sql = string.Format(@" DELETE FROM products WHERE product_id = {0} ", id);

            await OracleContextAsync.CreateUpdateDeleteAsync(sql);
        }

        public async Task UpdateAsync(ProductDal oldItem, ProductDal newItem)
        {
            var desc = oldItem.description != newItem.description ? newItem.description : "";
            var price = oldItem.price != newItem.price ? newItem.price : oldItem.price;
            var name = oldItem.name != newItem.name ? newItem.name : "";

            var updateSql = "";

            if (desc != "")
            {
                updateSql = String.Format(" SET description = '{0}' ", desc);
            }
            if (!Equals(price, oldItem.price))
            {
                updateSql += String.Format("{0} SET price = {1} ", updateSql != "" ? "," : "", price);
            }
            if (name != "")
            {
                updateSql = String.Format("{0} SET name = '{1}' ", updateSql != "" ? "," : "", name);
            }

            if (updateSql != "")
            {
                string sql = string.Format(@" UPDATE products 
                            {0}
                            WHERE product_id = {1} ",

                    updateSql,
                    newItem.product_id);

                await OracleContextAsync.CreateUpdateDeleteAsync(sql);
            }
        }
    }
}
