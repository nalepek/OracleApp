using System.Threading.Tasks;
using MediatR;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Application.Product
{
    public interface IProductCommandService
    {
        Task<ProductDal> Update(ProductDal productId);
        void Delete(int productId);
    }

    public class ProductCommandService : IProductCommandService
    {
        private readonly IMediator _mediator;

        public ProductCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductDal> Update(ProductDal product)
        {
            return await _mediator.Send(new ProductCommand(product));

        }

        public void Delete(int productId)
        {
            
        }
    }
}
