namespace Domain.DTOs;

public class SearchDayContentParametersDto
{
    public string? Username { get;}
    
    public int? UserId { get;}
    
    public DateOnly? Date { get; }
    
    
    public int? ProductId { get; }

    public SearchDayContentParametersDto(string? username, int? userId, DateOnly date, int productId)
    {
        Username = username;
        UserId = userId;
        Date = date;
        ProductId = productId;
    }
}