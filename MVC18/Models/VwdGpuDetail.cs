using System;
using System.Collections.Generic;

namespace MVC18.Models;

public partial class VwdGpuDetail
{
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

    public float MemorySize { get; set; }

    public string MemoryType { get; set; } = null!;

    public int Clock { get; set; }

    public int UnifiedShader { get; set; }

    public int Tmu { get; set; }

    public int Rop { get; set; }

    public string Bus { get; set; } = null!;

    public bool? Igpu { get; set; }

    public int GpuId { get; set; }
}
