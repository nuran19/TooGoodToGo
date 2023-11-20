using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public int Id { get; set; } // user name is unique, which might make the Id property redundant
    public string UserName { get; set; }
    public string Password { get; set; }

    //two way navigation property -efc
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
}