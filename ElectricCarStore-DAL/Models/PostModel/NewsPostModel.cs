using System.ComponentModel.DataAnnotations;

namespace ElectricCarStore_DAL.Models.PostModel
{
    public class NewsPostModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tin tức")]
        [StringLength(255, ErrorMessage = "Tên không được vượt quá 255 ký tự")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả ngắn không được vượt quá 255 ký tự")]
        public string Desc { get; set; }

        public int? ImageId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung tin tức")]
        public string Content { get; set; }
        public bool? IsAboutUs { get; set; }
    }

}
