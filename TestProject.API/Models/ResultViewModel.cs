using TestProject.Common.Filtering;

namespace TestProject.API.Models
{
    public class ResultViewModel
    {
        public int TotalCount { get; set; }
        public PageInfoModel PagingInfo { get; set; }
    }
}