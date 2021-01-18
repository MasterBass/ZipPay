using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TestProject.Common.Filtering
{
    public class PagedEntityResult<T>
    {
        [JsonProperty("result")]
        public IEnumerable<T> Result { get; set; }

        [JsonProperty("count")]
        public int TotalCount { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfoModel PagingInfo { get; set; }

        public PagedEntityResult() { }

        public PagedEntityResult(IQueryable<T> result, PageInfoModel pageModel)
        {
            PagingInfo = pageModel;
            TotalCount = result.Count();
            Result = new List<T>();
        }
    }
}