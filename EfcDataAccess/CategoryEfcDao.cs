using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class CategoryEfcDao : ICategoryDao
{
    
    private readonly TGTGContext context;

    public CategoryEfcDao(TGTGContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Category>> GetAsync()
    {
        
        IQueryable<Category> usersQuery = context.Category.AsQueryable();
        IEnumerable<Category> result = await usersQuery.ToListAsync();
        return result;
    }
    
}