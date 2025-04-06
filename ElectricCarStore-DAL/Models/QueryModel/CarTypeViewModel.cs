using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.QueryModel
{
    public class CarTypeViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; } // Thêm tên xe để dễ đọc
        public string TypeName { get; set; }
        public decimal Price { get; set; }
    }

}
