using System.ComponentModel.DataAnnotations;

namespace ElectricCarStore_DAL.Models.PostModel
{
    public class CarPostModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên xe")]
        [StringLength(255, ErrorMessage = "Tên xe không được vượt quá 255 ký tự")]
        public string Name { get; set; }

        public string DescriptionA { get; set; }

        public string DescriptionB { get; set; }
    }

}
