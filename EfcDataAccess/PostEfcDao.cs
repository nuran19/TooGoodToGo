using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    
   public async Task<Post> CreateAsync(Post post) //The method takes a post and returns a post (because the Id is set).
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    //view posts - search
   public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.Owner.Id == searchParameters.UserId);
        }
    
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchParameters.BodyContains))
        {
            query = query.Where(t =>
                t.Body.ToLower().Contains(searchParameters.BodyContains.ToLower()));
        }
        List<Post> result = await query.ToListAsync(); //executing against the database 
        return result;
    }
    
    //view single post  
   public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? found = await context.Posts
            .Include(post => post.Owner)
            .SingleOrDefaultAsync(post => post.Id == postId);
        return found;
    }
}