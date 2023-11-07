namespace Domain.Models;
// posts keep track of its assignee
//The Post has an Owner, which should reference the User to which this Post is "assigned"
public class Post
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public string Body { get; }

    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
}