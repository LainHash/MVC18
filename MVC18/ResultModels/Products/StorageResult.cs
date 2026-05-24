using MVC18.DTOs.Products;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class StorageResult : BaseResult
    {
        public StorageDTO? Storage { get; set; }
        public List<StorageDTO>? Storages { get; set; }
    }
}
