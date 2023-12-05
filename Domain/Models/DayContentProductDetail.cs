namespace Domain.Models;

public class DayContentProductDetail
{
    // This class is only used where data from product table is required to view - 
    public int DayContentId { get; set; } 
    public int ProductId { get; set; } 
    public string ProductName { get; set; }
    public string? Reason { get; set; }
    public int Quantity { get; set; }

    public DayContentProductDetail(int dayContentId, int productId, string productName, string? reason, int quantity)
    {
        DayContentId = dayContentId;
        ProductId = productId;
        ProductName = productName;
        Reason = reason;
        Quantity = quantity;
    }

    public DayContentProductDetail()
    {
    }
    
}