using MVC18.DTOs.Misc;

namespace MVC18.ResultModels.Misc
{
    public class CategoryResult : BaseResult
    {
        public CategoryDTO? Category { get; set; }
        public List<CategoryDTO>? Categories { get; set; }
    }
}
