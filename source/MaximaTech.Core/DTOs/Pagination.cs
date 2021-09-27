using System.Collections.Generic;

namespace MaximaTech.Core.DTOs
{
    public class Pagination<TItems>
    {
        public List<TItems> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}