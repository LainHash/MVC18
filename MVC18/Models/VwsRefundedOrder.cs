using System;
using System.Collections.Generic;

namespace MVC18.Models;

public partial class VwsRefundedOrder
{
    public int? Day { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public int? RefundedOrders { get; set; }

    public decimal? TotalRevenue { get; set; }

    public int? TotalQuantity { get; set; }
}
