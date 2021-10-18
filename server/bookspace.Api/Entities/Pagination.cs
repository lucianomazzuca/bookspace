using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public class Pagination<BaseEntity>
    {
        public Pagination(List<BaseEntity> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Count = count;
            Items = items;
        }

        public List<BaseEntity> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get;  set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
