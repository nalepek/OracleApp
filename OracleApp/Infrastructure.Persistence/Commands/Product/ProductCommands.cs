using MediatR;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.Commands.Product
{
    public class ProductCommand
    {
        public ProductDal Product { get; private set; }

        public ProductCommand(ProductDal product)
        {
            Product = product;
        }
    }

    public class UpdateProductCommand : ProductCommand, IRequest<ProductDal>
    {
        public UpdateProductCommand(ProductDal product) : base(product)
        {
        }
    }

    public class AddProductCommand : ProductCommand, IRequest
    {
        public AddProductCommand(ProductDal product) : base(product)
        {
        }
    }

    public class DeleteProductCommand : IRequest
    {
        public decimal ProductId { get; set; }

        public DeleteProductCommand(decimal productId)
        {
            ProductId = productId;
        }
    }
}
