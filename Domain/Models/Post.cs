namespace Domain.Models;
// posts keep track of its assignee
//The Post has an Owner, which should reference the User to which this Post is "assigned"
public class Post
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public int OwnerId { get; set; } //an explicit foreign key,EFC will identify this as an FK because of naming conventions.
    public string Title { get; private set; }
    public string Body { get; private set; }

    // public Post(User owner, string title, string body)
    // {
    //     Owner = owner;
    //     Title = title;
    //     Body = body;
    // }
    public Post(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
    }
   

    //no-arguments constructor for EFC to call when creating objects
    private Post() { }
}