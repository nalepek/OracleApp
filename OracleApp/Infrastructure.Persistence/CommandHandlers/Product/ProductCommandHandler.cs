using System.Threading.Tasks;
using MediatR;
using OracleApp.Common.CommandHandlers;
using OracleApp.Infrastructure.Persistence.Commands.Product;
using OracleApp.Infrastructure.Persistence.Dal.Product;

namespace OracleApp.Infrastructure.Persistence.CommandHandlers.Product
{
    public class ProductCommandHandler : BaseAbstractCommandHandler<ProductCommand, ProductDal>
    {
        protected override async Task<ProductDal> HandleCore(ProductCommand command)
        {
            OracleContextAsync.QueryForObjAsync(sql);

            var sql = string.Concat(@"UPDATE products "
                        
                            SET ");

            OracleContext.CreateUpdateDelete(sql);

            return await OracleContextAsync.QueryForObjAsync<ProductDal>(sql);
        }
    }
}
