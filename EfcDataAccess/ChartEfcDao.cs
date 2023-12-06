using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class ChartEfcDao :IChartDao
{
    private readonly TGTGContext context;

    public ChartEfcDao(TGTGContext context)
    {
        this.context = context;
    }
    
    public async Task<IEnumerable<DayContentProduct>> GetAsync(SearchChartParametersDto searchParams)
    {
       
         IQueryable <DayContentProduct> query = context.DayContentProduct
             .Include(c => c.Product)
             .Include(c=> c.DayContent)  
            .AsQueryable();
     
         if (searchParams.CompanyId != null) 
         {
            query = query.Where( c => c.Product.Owner.CompanyId == searchParams.CompanyId); 
         }
         if (!string.IsNullOrEmpty(searchParams.ProductTypeContains)) 
         {
            
            query = query.Where(p=> p.Product.Type.ToLower().Equals(searchParams.ProductTypeContains.ToLower()));  //??????????????????????
         }
        // Console.WriteLine(query.ToQueryString());
         List<DayContentProduct> result = await query.ToListAsync();
        return result;
        
    }

    public async Task<IEnumerable<DayContentProduct>> GetAsyncMore(SearchChartParametersDto searchParams)
    {
       
        IQueryable <DayContentProduct> query = context.DayContentProduct
            .Include(c => c.Product)
            .Include(c=> c.DayContent)   
            .AsQueryable();
     
        if (searchParams.CompanyId != null)
        {
            query = query.Where( c => c.Product.Owner.CompanyId == searchParams.CompanyId);
        }
        //
         if (!string.IsNullOrEmpty(searchParams.CategoryContains))
         {
           query = query.Where(p => p.Product.SubCategory.Category.Name.ToLower().Equals(searchParams.CategoryContains.ToLower()) ); 
         }
        if (!string.IsNullOrEmpty(searchParams.SubCategoryContains))
        {
            query = query.Where(p=> p.Product.SubCategory.Name.ToLower().Equals(searchParams.SubCategoryContains.ToLower()));  //??????????????????????
        }
         
        if (!string.IsNullOrEmpty(searchParams.ProductTypeContains))
        {
            
            query = query.Where(p=> p.Product.Type.ToLower().Equals(searchParams.ProductTypeContains.ToLower()));  //??????????????????????
        }
        // Console.WriteLine(query.ToQueryString());
        List<DayContentProduct> result = await query.ToListAsync();
        return result;
        
    }
}