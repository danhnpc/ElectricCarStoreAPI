using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.PostModel
{
    public class CarTypePostModel
    {
        [Required(ErrorMessage = "Vui lòng chọn xe")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên loại xe")]
        [StringLength(255, ErrorMessage = "Tên loại xe không được vượt quá 255 ký tự")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0, 9999999999.99, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        [Precision(10, 2)]
        public decimal Price { get; set; }
    }

}
