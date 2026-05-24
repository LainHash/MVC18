using Microsoft.AspNetCore.Mvc.Rendering;
using MVC18.Data;
using MVC18.ResultModels.Misc;

namespace MVC18.Services.Interfaces.Commons
{
    public interface ICommonService
    {
        CategoryResult GetAllCategories();
        SupplierResult GetAllSuppliers(string? categoryName);

        List<SelectListItem> GetAllSortByOptions();
    }
}
