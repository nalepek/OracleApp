using OracleApp.Common.Repositories;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Repositories.Product
{
    public interface IProductRepository : IAsyncRepository<ProductDal>
    {
    }
}
