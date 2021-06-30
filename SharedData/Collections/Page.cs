using System.Collections.Generic;

namespace SharedData.Collections
{
    public class Page<E> where E : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IList<E> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}