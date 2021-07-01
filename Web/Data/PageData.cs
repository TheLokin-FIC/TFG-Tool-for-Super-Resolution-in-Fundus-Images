using SharedData.MachineLearning;
using System.Collections.Generic;

namespace Web.Data
{
    public class PageData
    {
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IList<ModelDetails> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PageData()
        {
            SearchTerm = "";
            PageSize = 3;
            PageIndex = 1;
            TotalPages = 0;
            Items = new List<ModelDetails>();
            HasPreviousPage = false;
            HasNextPage = false;
        }
    }
}