using Domain.Models;

namespace Domain.DTOs;

// Object of this class loads with data from Database against a quary.
public class DayContentBasicDto
{
    public int Id { get;  }
    
    
    public DateOnly Date { get;  }
    
    

    public DayContentBasicDto(int id, DateOnly date)
    {
        Id = id;
        Date = date;
    }
}