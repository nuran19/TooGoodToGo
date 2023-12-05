using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess;

public class DayContentEfcDao : IDayContentDao
{
    private readonly TGTGContext context;

    public DayContentEfcDao(TGTGContext context)
    {
        this.context = context;
    }

    public async Task<DayContent> CreateAsync(DayContent dayContent)
    {
         // saving day content and getting its ID
        EntityEntry<DayContent> newDayContent = await context.DayContent.AddAsync(dayContent);
        await context.SaveChangesAsync(); 
        return newDayContent.Entity; 
    }

    public async Task<IEnumerable<DayContent>> GetAsync(SearchDayContentParametersDto searchParameters)
    {
        IQueryable <DayContent>query = context.DayContent.Include(dayContent => dayContent.Owner).AsQueryable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(product =>
                product.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }

        if (searchParameters.UserId != null)
        {
            query = query.Where(d => d.Owner.Id == searchParameters.UserId);
            
        }

        if (searchParameters.Date != null)
        {
            query = query.Where(d => d.Date == searchParameters.Date);
        }
        

        IEnumerable<DayContent> result = await query.ToListAsync();
        
        return result;
    }
    //view single dayContent
    public async Task<DayContent?> GetByIdAsync(int dayContentid)
    {
        DayContent? found = await context.DayContent
            .Include(dayContent => dayContent.Owner)
            .SingleOrDefaultAsync(dayContent => dayContent.Id == dayContentid);
        return found;
    }
}