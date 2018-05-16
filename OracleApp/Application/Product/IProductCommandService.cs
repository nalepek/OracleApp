using System.Threading.Tasks;
using MediatR;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Application.Product
{
    public interface IProductCommandService
    {
        Task<ProductDal> UpdateAsync(ProductDal product);
        Task DeleteAsync(decimal productId);
        Task AddAsync(ProductDal product);
    }

    public class ProductCommandService : IProductCommandService
    {
        private readonly IMediator _mediator;

        public ProductCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductDal> UpdateAsync(ProductDal product)
        {
            return await _mediator.Send(new UpdateProductCommand(product));
        }

        public async Task DeleteAsync(decimal productId)
        {
            await _mediator.Send(new DeleteProductCommand(productId));
        }

        public async Task AddAsync(ProductDal product)
        {
            await _mediator.Send(new AddProductCommand(product));
        }
    }
}
