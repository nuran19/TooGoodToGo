using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Domain.Models;


namespace Shared.Models;

public class DayContent
{
    [Key]
    public int Id { get; set; } 
    
    public DateOnly Date { get; set; }
    
    public User Owner { get; private set; }
    
    public int UserId { get; set; }
    
    public virtual ICollection<DayContentProduct> DayContentProducts { get; set; } = new List<DayContentProduct>(); 
    
    
    public DayContent(int userId)
    {
        Date = DateOnly.FromDateTime(DateTime.Now); 
        UserId = userId;
    }

    public DayContent(int userId, DateOnly date, ICollection<DayContentProduct> dayContentProducts)
    {
        UserId = userId;
        Date = date;
        DayContentProducts = dayContentProducts;
    }

    public DayContent() {}
}