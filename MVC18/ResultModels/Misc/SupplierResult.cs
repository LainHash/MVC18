using MVC18.DTOs.Misc;

namespace MVC18.ResultModels.Misc
{
    public class SupplierResult : BaseResult
    {
        public SupplierDTO? Supplier { get; set; }
        public List<SupplierDTO>? Suppliers { get; set; }
    }
}
