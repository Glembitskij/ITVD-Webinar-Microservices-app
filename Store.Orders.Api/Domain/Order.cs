public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Confirmed";
    public int ProductId { get; set; }
    public decimal AppliedPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount => AppliedPrice * Quantity;
}