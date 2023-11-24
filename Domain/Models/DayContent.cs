using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Models;


namespace Shared.Models;

public class DayContent
{
    [Key]
    public int Id { get; set; }
    
    [Column(TypeName="Date")]
    public DateOnly Date { get; set; }
    [AllowNull]
    public string Reason { get; set; }
    public int Quantity { get; set; } //Product's quantity
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }

    public ICollection<Product> Products { get; set; }


    public DayContent(int productId, int quantity,  string reason)
    {
        ProductId = productId;
        Quantity = quantity;
        Date = DateOnly.FromDateTime(DateTime.Now);
        Reason = reason;
    }

    private DayContent() {}
}