namespace Domain.DTOs;

public class DayContentProductUpdateDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
}