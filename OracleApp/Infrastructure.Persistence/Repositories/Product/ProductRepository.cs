using System.Threading.Tasks;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        public async Task DeleteAsync(decimal id)
        {
            string sql = string.Format(@" DELETE FROM products WHERE product_id = {0} ", id);

            var result = await OracleContextAsync.CreateUpdateDeleteAsync(sql);

        }

        public async Task<int> UpdateAsync(ProductDal oldItem, ProductDal newItem)
        {
            var desc = oldItem.description != newItem.description ? newItem.description : oldItem.description;
            var price = oldItem.price != newItem.price ? newItem.price : oldItem.price;
            var name = oldItem.name != newItem.name ? newItem.name : oldItem.name;

            string sql = string.Format(@" UPDATE products 
                            SET price = {0} ,
                            SET name = {1} ,
                            SET description = {2} 
                            WHERE product_id = {3} ",

                            price,
                            name,
                            desc,
                            newItem.product_id);

            var product = await OracleContextAsync.CreateUpdateDeleteAsync(sql);

            return product;
        }
    }
}
