using System.Threading.Tasks;
using MediatR;
using OracleApp.Common.CommandHandlers;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Repositories.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Infrastructure.Persistence.CommandHandlers.Product
{
    public class ProductCommandHandler : BaseAbstractCommandHandler<ProductCommand, ProductDal>
    {
        private readonly IProductSearcher _productSearcher;
        private readonly IProductRepository _productRepository;

        public ProductCommandHandler(IProductSearcher productSearcher, IProductRepository productRepository)
        {
            _productSearcher = productSearcher;
            _productRepository = productRepository;
        }

        protected override async Task<ProductDal> HandleCore(ProductCommand command)
        {
            var oldProduct = _productSearcher.Get(command.Product.product_id);

            var x = await _productRepository.UpdateAsync(oldProduct, command.Product);
            return new ProductDal();
        }
    }
}
