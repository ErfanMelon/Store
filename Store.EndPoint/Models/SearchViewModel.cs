using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.EndPoint.Models
{
    public class SearchViewModel
    {
        public long CategoryId { get; set; }
        public SelectList Categories { get; set; }
        public string SearchKey { get; set; }
    }
}
