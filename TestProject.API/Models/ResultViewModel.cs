using TestProject.Common.Filtering;

namespace TestProject.API.Models
{
    public abstract class ResultViewModel
    {
        public int TotalCount { get; set; }
        public PageInfoModel PagingInfo { get; set; }
    }
}