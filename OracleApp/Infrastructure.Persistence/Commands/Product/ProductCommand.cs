using MediatR;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Commands.Product
{
    public class ProductCommand : IRequest<ProductDal>
    {
        public int ProductId { get; private set; }

        public ProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
