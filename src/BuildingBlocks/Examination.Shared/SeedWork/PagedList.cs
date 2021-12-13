using System;
using System.Collections.Generic;

namespace Examination.Shared.SeedWork
{
    public class PagedList<T>
    {
        public MetaData MetaData { get; set; }

        public List<T> Items { get; set; }

        public PagedList()
        {

        }

        public PagedList(List<T> items, long count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}
