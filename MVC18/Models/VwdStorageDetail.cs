using System;
using System.Collections.Generic;

namespace MVC18.Models;

public partial class VwdStorageDetail
{
    public int Capacity { get; set; }

    public string MemoryType { get; set; } = null!;

    public string InterfaceType { get; set; } = null!;

    public int ReadSpeed { get; set; }

    public int WriteSpeed { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public Guid ProductUuid { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public bool Discontinued { get; set; }

    public int ProductId { get; set; }

    public int ProductSkuId { get; set; }

    public int StorageId { get; set; }
}
