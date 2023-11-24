using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Models;


namespace Shared.Models;

public class DayContent
{
    [Key]
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public string? Reason { get; set; }
    public int Quantity { get; set; } //Product's quantity
    

    public ICollection<Product> Products { get; set; }
    public ICollection<User> Users { get; set; }


    public DayContent(int quantity,  string reason)
    {
        Quantity = quantity;
        Date = DateOnly.FromDateTime(DateTime.Now);
        Reason = reason;
    }

    private DayContent() {}
}