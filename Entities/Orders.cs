namespace sew.Entities;

public class Orders
{
    public int OrderID { get; set; }
    public int? CustomerID { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public string? OrderStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public bool? IsPaid { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? Archived { get; set; }
}
