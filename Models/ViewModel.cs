namespace sew.Models;

public class ViewModel
{
    public class UsersView
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte Gender { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
    }

    public class CustomersView
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

    public class OrdersView
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
}
