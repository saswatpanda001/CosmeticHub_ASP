using System;
using System.Collections.Generic;

namespace CosmeticHub.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceEach { get; set; }
}
