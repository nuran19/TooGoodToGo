using Domain.Models;

namespace Domain.DTOs;

public class DayContentCreationDto
{
    
    public int OwnerId { get;  }
    public DateOnly Date { get; }
    
    public ICollection<DayContentProduct> DayContentProducts { get; set; }
    


    public DayContentCreationDto(int ownerId,DateOnly date, ICollection<DayContentProduct> dayContentProducts)
    {
        OwnerId = ownerId;
        Date = date;
        DayContentProducts = dayContentProducts;
        
    }
}