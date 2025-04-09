using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.PostModel
{
    public class CarDetailPostModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên xe")]
        [StringLength(255, ErrorMessage = "Tên xe không được vượt quá 255 ký tự")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả A không được vượt quá 500 ký tự")]
        public string DescriptionA { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả B không được vượt quá 500 ký tự")]
        public string DescriptionB { get; set; }

        // Danh sách ID hình ảnh
        public List<int> ImageIds { get; set; } = new List<int>();

        // Danh sách các loại xe (tùy chọn)
        public List<CarTypePostModel> CarTypes { get; set; } = new List<CarTypePostModel>();
    }
}
