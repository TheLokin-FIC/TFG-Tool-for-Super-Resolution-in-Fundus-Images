using System.Collections.Generic;

namespace Repository.DAOs.Collections
{
    public interface IPageList<E> where E : class
    {
        public int PageSize { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public IList<E> Items { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}