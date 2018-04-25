using OracleApp.Common.Searchers;

namespace OracleApp.Infrastructure.Persistence.Searchers.Product
{
    public class ProductSearchCriteria : Criteria
    {
        public int? ProductId { get; set; }
    }
}
