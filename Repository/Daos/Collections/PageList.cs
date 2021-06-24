using System.Collections.Generic;

namespace Repository.DAOs.Collections
{
    public class PageList<E> : IPageList<E> where E : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IList<E> Items { get; set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}