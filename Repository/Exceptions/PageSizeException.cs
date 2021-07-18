using System;

namespace Repository.Exceptions
{
    public class PageSizeException : Exception
    {
        public int PageSize { get; }

        public PageSizeException(int pageSize) : base($"Page size must be greater than zero (pageSize={pageSize})")
        {
            PageSize = pageSize;
        }
    }
}