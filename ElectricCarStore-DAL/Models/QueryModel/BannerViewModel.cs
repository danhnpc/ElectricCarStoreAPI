using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.QueryModel
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // URL hình ảnh
    }
}
