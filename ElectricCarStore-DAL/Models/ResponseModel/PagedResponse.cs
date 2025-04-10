using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.ResponseModel
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PerPage);
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
    }
}
