using System.Collections.Generic;

namespace OracleApp.Common.Searchers
{
    public abstract class SearchResult <TDal>
    {
        public IList<TDal> Items { get; set; }
        public decimal Count { get; set; }
    }
}
