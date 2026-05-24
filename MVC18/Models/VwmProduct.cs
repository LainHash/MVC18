using System;
using System.Collections.Generic;

namespace MVC18.Models;

public partial class VwmProduct
{
    public Guid ProductUuid { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public bool Discontinued { get; set; }

    public bool Expr1 { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ImageUrl { get; set; }
}
