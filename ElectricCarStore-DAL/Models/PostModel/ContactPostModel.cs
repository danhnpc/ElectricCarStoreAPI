using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models.PostModel
{
    public class ContactPostModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(255, ErrorMessage = "Họ tên không được vượt quá 255 ký tự")]
        public string FullName { get; set; }

        public int? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [StringLength(255, ErrorMessage = "Nội dung không được vượt quá 255 ký tự")]
        public string Content { get; set; }

        public int? CarId { get; set; }
    }

}
