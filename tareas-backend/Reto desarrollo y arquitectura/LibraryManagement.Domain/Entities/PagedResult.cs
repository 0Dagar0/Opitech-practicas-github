using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>(); // los datos de la página actual.
        public int Page { get; set; } // el número de página actual (1-based).
        public int PageSize { get; set; } // el número de elementos por página.
        public int TotalCount { get; set; }// el número total de elementos en la colección completa.
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);// el número total de páginas, calculado a partir de TotalCount y PageSize.
    }
}

