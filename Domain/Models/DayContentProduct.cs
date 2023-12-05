using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTOs;
using Shared.Models;

namespace Domain.Models;

public class DayContentProduct
{
    [ForeignKey("DayContentId")]
    public int DayContentId { get; set; }
   
    public int ProductId { get; set; }
    
    
    public string? Reason { get; set; }
    public int Quantity { get; set; } 
    

    public DayContentProduct(int dayContentId, int productId, int quantity, string? reason)
    {
        DayContentId = dayContentId;
        ProductId = productId;
        Quantity = quantity;
        Reason = reason;
    }
  
    private DayContentProduct() {}

    
}