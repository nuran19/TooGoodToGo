using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class SubCategoryEfcDao : ISubCategoryDao
{

    private readonly TGTGContext context;

    public SubCategoryEfcDao(TGTGContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<SubCategory>> GetAsync(int catId)
    {
        // IQueryable<SubCategory> subCategoriesQuery = context.SubCategory.AsQueryable();
        // if (searchParameters.CategoryIdContains != null)
        // {
        //     subCategoriesQuery = subCategoriesQuery.Where(u => u.CategoryId ==searchParameters.CategoryIdContains);
        // }
        // IEnumerable<SubCategory> result = await subCategoriesQuery.ToListAsync();
        // return result;
        
        IQueryable<SubCategory> subcategoriesQuery = context.SubCategory.Include (sc => sc.Category).AsQueryable();
        if ( catId != 0)
        {
            subcategoriesQuery = subcategoriesQuery.Where(u => u.Category.Id == catId);
        }
   
        List<SubCategory> result = await subcategoriesQuery.ToListAsync();
        return result;
    }

}