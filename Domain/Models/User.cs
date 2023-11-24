using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    
    public int Id { get; set; } // user name is unique, which might make the Id property redundant
    public string UserName { get; set; }
    public string Password { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    public string Role { get; set;}
    
    

    //two way navigation property -efc
    [JsonIgnore]
    public ICollection<Product> Products { get; set; }
}