using Newtonsoft.Json;

namespace TestProject.Common.Filtering
{
    public class PageInfoModel
    {
        
        [JsonProperty("pageSize")]
        public int? PageSize { get; set; } = 10;

        [JsonProperty("pageNumber")]
        public int? PageNumber { get; set; } = 0;

        [JsonProperty("sortDir")]
        public string SortDir { get; set; }

        [JsonProperty("sortKey")]
        public string SortKey { get; set; }
    }
}