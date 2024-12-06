using System;
using System.Collections.Generic;

namespace CosmeticHub.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int OrderRec { get; set; }

    public int UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime? PlacedAt { get; set; }

    public DateTime? ShippedAt { get; set; }
}
