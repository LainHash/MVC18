using System.ComponentModel.DataAnnotations;

namespace MVC18.ViewModels
{
    public class CategoryMenu
    {
        [Display(Name = "Tên Danh Mục")]
        public string Name { get; set; } = null!;

        [Display(Name = "Số Lượng Sản Phẩm")]
        public int Count { get; set; } = 0;
    }
}
