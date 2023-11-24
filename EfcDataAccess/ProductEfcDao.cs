using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class ProductEfcDao : IProductDao
{
    private readonly TGTGContext context;

    public ProductEfcDao(TGTGContext context)
    {
        this.context = context;
    }
    
   public async Task<Product> CreateAsync(Product product) //The method takes a product and returns a product (because the Id is set).
    {
        EntityEntry<Product> added = await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    //view products - search??????????????????????????????????????????
   public async Task<IEnumerable<Product>> GetAsync(SearchProductParametersDto searchParameters)  
    {
        IQueryable<Product> query = context.Products.Include (product => product.Owner).AsQueryable(); 
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(product =>
                product.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.Owner.Id == searchParameters.UserId);
        }
        if (searchParameters.CompanyId != null)
        {
            query = query.Where(t => t.Owner.CompanyId == searchParameters.CompanyId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.SubCategoryContains))
        {
            query = query.Where(t =>
                t.SubCategory.Name.ToLower().Contains(searchParameters.SubCategoryContains.ToLower()));
        }
        if (!string.IsNullOrEmpty(searchParameters.TypeContains))
        {
            query = query.Where(t =>
                t.Type.ToLower().Contains(searchParameters.TypeContains.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchParameters.BrandContains))
        {
            query = query.Where(t =>
                t.Brand.ToLower().Contains(searchParameters.BrandContains.ToLower()));
        }
        
        List<Product> result = await query.ToListAsync(); //executing against the database 
        return result;
    }
    
    //view single product  
   public async Task<Product?> GetByIdAsync(int productId)
    {
        Product? found = await context.Products
            .Include(product => product.Owner) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
           .Include (product => product.SubCategory)  // to access subcat name!!!!!
            .SingleOrDefaultAsync(product => product.Id == productId);
        return found;
    }
}