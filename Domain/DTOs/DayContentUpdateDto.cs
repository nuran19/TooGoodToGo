namespace Domain.DTOs;

public class DayContentUpdateDto
{
    public int Id { get;  }
    
    
    public DateOnly? Date { get; set; }
    public List<DayContentProductUpdateDto> Products { get; set; }

    
    

    public DayContentUpdateDto(int id)
    {
        Id = id;
        
    }
}