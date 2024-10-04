using System;

namespace sew.Entities;

public class Customers
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public byte? Gender { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PinCode { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
}
