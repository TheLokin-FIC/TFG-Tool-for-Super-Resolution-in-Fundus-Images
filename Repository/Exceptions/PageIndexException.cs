using System;

namespace Repository.Exceptions
{
    public class PageIndexException : Exception
    {
        public int PageIndex { get; }

        public PageIndexException(int pageIndex, int totalPages) : base($"Page index outside of total pages [0, {totalPages}] (pageIndex={pageIndex})")
        {
            PageIndex = pageIndex;
        }
    }
}