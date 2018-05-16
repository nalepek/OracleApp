using System;
using System.Linq;
using System.Threading.Tasks;
using OracleApp.Common.CommandHandlers;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;
using OracleApp.Infrastructure.Persistence.Repositories.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;

namespace OracleApp.Infrastructure.Persistence.CommandHandlers.Product
{
    public class UpdateProductCommandHandler : BaseAbstractCommandHandler<UpdateProductCommand, ProductDal>
    {
        private readonly IProductSearcher _productSearcher;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductSearcher productSearcher, IProductRepository productRepository)
        {
            _productSearcher = productSearcher;
            _productRepository = productRepository;
        }

        protected override async Task<ProductDal> HandleCore(UpdateProductCommand command)
        {
            var oldProduct = await _productSearcher.GetAsync(command.Product.product_id);

            await _productRepository.UpdateAsync(oldProduct, command.Product);

            return await _productSearcher.GetAsync(command.Product.product_id);
        }
    }

    public class AddProductCommandHandler : BaseAbstractCommandHandler<AddProductCommand>
    {
        private readonly IProductSearcher _productSearcher;
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductSearcher productSearcher, IProductRepository productRepository)
        {
            _productSearcher = productSearcher;
            _productRepository = productRepository;
        }

        protected override async Task HandleCore(AddProductCommand command)
        {
            var products = await _productSearcher.SearchAsync(new ProductSearchCriteria
            {
                Sort = "product_id",
                PageSize = Int32.MaxValue
            });

            var last = products.Items.LastOrDefault();

            if (last != null)
                command.Product.product_id = last.product_id + 1;
            else
                command.Product.product_id = 1;

            await _productRepository.AddAsync(command.Product);
        }
    }

    public class DeleteProductCommandHandler : BaseAbstractCommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        protected override async Task HandleCore(DeleteProductCommand command)
        {
            await _productRepository.DeleteAsync(command.ProductId);
        }
    }
}
