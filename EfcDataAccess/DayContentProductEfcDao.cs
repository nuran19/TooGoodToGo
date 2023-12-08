using System.Data;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess;

public class DayContentProductEfcDao:IDayContentProductDao
{
    private readonly TGTGContext context;

    public DayContentProductEfcDao(TGTGContext context)
    {
        this.context = context;
    }
    
    public async Task<DayContentProduct> CreateAsync(DayContentProduct dayContentProduct)
    {
        EntityEntry<DayContentProduct> newDayContentProduct = await context.DayContentProduct.AddAsync(dayContentProduct);
        await context.SaveChangesAsync(); 
        return newDayContentProduct.Entity; 
    }
    
    public async Task<ICollection<DayContentProduct>> CreateAsync(int dayContentId, List<DayContentProduct> dayContentProducts)
    {
        ICollection<DayContentProduct> dcProductsCreated = new List<DayContentProduct>();
        
        foreach (var product in dayContentProducts)
        {
            DayContentProduct dcProduct = new DayContentProduct(dayContentId, product.ProductId, product.Quantity, product.Reason);
            
            // Check if the entity is already tracked
            var existingEntry = context.ChangeTracker.Entries<DayContentProduct>().FirstOrDefault(
                e => e.Entity.DayContentId == dcProduct.DayContentId && e.Entity.ProductId == dcProduct.ProductId);

            if (existingEntry != null)
            {
                // Entity is already tracked, no need to attach
                dcProductsCreated.Add(existingEntry.Entity);
            }
            else
            {
                // Entity is not tracked, attach it
                context.DayContentProduct.Attach(dcProduct);
                dcProductsCreated.Add(dcProduct);
            }
        }

        await context.SaveChangesAsync();  
        return dcProductsCreated;
    }

    
    public async Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId)
    {
        try
        {
            // Logic to retrieve day content products based on dayContentId 
            var dayContentProducts = await context.DayContentProduct
                .Where(dc => dc.DayContentId == dayContentId)
                .ToListAsync(); 
            return dayContentProducts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw; 
        }
    } 
    
    public async Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId)
    {
        try
        { 
            var dayContentProducts = (
                from dc in context.DayContentProduct
                join p in context.Products on dc.ProductId equals p.Id 
                where (dc.DayContentId == dayContentId)
                select new DayContentProductDetail
                { DayContentId =  dc.DayContentId,
                    ProductId = p.Id,
                    Quantity = dc.Quantity,
                    Reason = dc.Reason,
                    ProductName = p.Type
                });

            return dayContentProducts.ToList();
        }
        catch (Exception e)
        {
            
            Console.WriteLine(e);
            throw; 
        }
    }

    

    public async Task DeleteAsync(DayContentProduct dayContentProduct)
    {
        DayContentProduct dayContentProductToDelete = context.DayContentProduct
            .FirstOrDefault(dc => dc.DayContentId == dayContentProduct.DayContentId && 
                         dc.ProductId == dayContentProduct.ProductId);

        if (dayContentProductToDelete == null)
        {
            throw new Exception($"DayContent with product ID {dayContentProduct.ProductId} not found.");
        }

        context.DayContentProduct.Remove(dayContentProductToDelete);
        await context.SaveChangesAsync();
        
        /*
        if (existingEntities.Any())
        {
            // If the entities are found, remove them
            context.DayContentProduct.RemoveRange(existingEntities);
            await context.SaveChangesAsync();
        }
        else
        {
            
            throw new Exception($"DayContentProduct with DayContentId {dayContentProduct.DayContentId} and ProductId {dayContentProduct.ProductId} not found.");
        }*/
    }
    
    

    public async Task<DayContentProduct?> GetByIdAsync(int dayContentId, int productId)
    {
        return await context.DayContentProduct.FindAsync(dayContentId,productId);
    }

    public async Task UpdateAsync(DayContentProduct dayContentProduct)
    {
        context.DayContentProduct.Update(dayContentProduct);
        await context.SaveChangesAsync();
    }
    
    

    private async Task<DayContentProduct?> GetExistingProductAsync(int productId, int dayContentId)
    {
        return await context.DayContentProduct
            .FirstOrDefaultAsync(p => p.ProductId == productId && p.DayContentId == dayContentId);
    }
    
}
     