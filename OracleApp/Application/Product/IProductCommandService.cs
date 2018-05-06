using System.Threading.Tasks;
using MediatR;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Application.Product
{
    public interface IProductCommandService
    {
        Task<ProductDal> Update(int productId);
        void Delete(int productId);
    }

    public class ProductCommandService : IProductCommandService
    {
        private readonly IMediator _mediator;

        public ProductCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductDal> Update(int productId)
        {
            return await _mediator.Send(new ProductCommand(productId));

        }

        public void Delete(int productId)
        {
            
        }
    }
}
