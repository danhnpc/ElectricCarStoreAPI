using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.QueryModel
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionA { get; set; }
        public string DescriptionB { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
        public List<CarTypeViewModel> CarTypes { get; set; } = new List<CarTypeViewModel>();
    }

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

}
