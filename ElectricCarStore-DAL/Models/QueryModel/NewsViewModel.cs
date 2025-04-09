using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.QueryModel
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
