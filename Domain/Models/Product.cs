using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Shared.Models;

namespace Domain.Models;
// posts keep track of its assignee
//The Product has an Owner, which should reference the User to which this Product is "assigned"
public class Product
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public SubCategory SubCategory { get; private set; }
    public int OwnerId { get; set; } //an explicit foreign key,EFC will identify this as an FK because of naming conventions.
    public int CompanyId { get; set; }  
    public int SubCategoryId { get; set; } 
    public string Type { get; private set; }
    public string Brand { get; private set; }
    public int Qty { get; private set; }
    public bool? IsEco { get; private set; } //??priv set??  //initially false by not setting
    
    public ICollection<DayContent> DayContents { get; set; }
    
    
    //????for creating products error added jsonConstructor
    [JsonConstructor]
    public Product(int ownerId,int companyId, int subCategoryId, string type, string brand, int qty)
    {
        OwnerId = ownerId;
        CompanyId = companyId;
        SubCategoryId = subCategoryId;//?????
        Type = type;
        Brand = brand;
        Qty = qty;
    }
   

    //no-arguments constructor for EFC to call when creating objects
    private Product() { }
}