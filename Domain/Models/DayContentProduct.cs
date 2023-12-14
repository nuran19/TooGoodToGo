using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Shared.Models;

namespace Domain.Models;

public class DayContentProduct
{
    // [ForeignKey("DayContentId")]
    public int DayContentId { get; set; }
   // [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public string? Reason { get; set; }
    public int Quantity { get; set; } 
    
    //navigation path DATA ANALYSIS
    public Product? Product { get; set; } 
   public DayContent? DayContent { get; set; } 
   
    
   // [JsonConstructor]
    public DayContentProduct(int dayContentId, int productId,int quantity, string? reason)
    {
        DayContentId = dayContentId;
        ProductId = productId;
        Quantity = quantity;
        Reason = reason;
    }
    
    public DayContentProduct() {}
}