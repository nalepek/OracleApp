using Microsoft.AspNetCore.Mvc;

namespace OracleApp.Common.Searchers
{
    public class Criteria
    {
        public Criteria()
        {
            PageSize = 10;
            Page = 0;
            Order = "asc";
        }

        [FromQuery(Name = "order")]
        public string Order { get; set; }

        [FromQuery(Name = "sort")]
        public string Sort { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
