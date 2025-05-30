using System.Collections.Generic;

namespace SimpleBlog.Core.Dtos
{
    public class PagedResultDto<T> // T va fi UserDto în cazul nostru
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // Numărul total de itemi (nu doar pe pagina curentă)
        public int TotalPages => (TotalCount + PageSize - 1) / PageSize; // Calculăm numărul total de pagini

        public PagedResultDto() { } // Constructor gol necesar uneori

        public PagedResultDto(List<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}