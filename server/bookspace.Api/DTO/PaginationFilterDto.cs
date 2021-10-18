using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.DTO
{
    public class PaginationFilterDto
    {
        public PaginationFilterDto()
        {
            PageNumber = 1;
            PageSize = 100;
        }

        public PaginationFilterDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 100 ? pageSize : 100;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
