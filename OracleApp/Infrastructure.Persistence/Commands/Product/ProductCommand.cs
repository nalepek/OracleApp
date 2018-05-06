using MediatR;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Commands.Product
{
    public class ProductCommand : IRequest<ProductDal>
    {
        public ProductDal Product { get; private set; }

        public ProductCommand(ProductDal product)
        {
            Product = product;
        }
    }
}
