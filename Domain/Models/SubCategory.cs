using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class SubCategory
{
    
    public int Id { get; set; }
    public string Name { get; set; }
 //   public int CategoryId { get; set; } add category
 
    
    public SubCategory(string name)
    {
        Name = name;
    }
}