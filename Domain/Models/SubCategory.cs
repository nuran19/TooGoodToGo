using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class SubCategory
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; } 
 
    //  public ICollection<Product> Products { get; set; } //todo add to represent the 1 to many relationship?
     
    public SubCategory(string name)
    {
        Name = name;
    }
}