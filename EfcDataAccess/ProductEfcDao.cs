using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

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
    //view products - //todo search bar
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

        if (searchParameters.DayContentId != null)
        {
            // Get all productIds relating to the given dayContentId
            List<int> productIds = context.DayContentProduct
                .Where(dcp => dcp.DayContentId == searchParameters.DayContentId)
                .Select(dcp => dcp.ProductId)
                .Distinct()
                .ToList();
            
            // Filter products query to only return products where the ID
            // relates to our dayContentId
            query = query.Where(prod => productIds.Contains(prod.Id));
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
            .ThenInclude(product=> product.Category)
            .SingleOrDefaultAsync(product => product.Id == productId);
        return found;
    }
   
   //delete product
   public async Task DeleteAsync(int id)
   {
       Product? existing = await GetByIdAsync(id);
       if (existing == null)
       {
           throw new Exception($"Product with id {id} not found");
       }

       context.Products.Remove(existing);
       await context.SaveChangesAsync();
   }
   
   //drop down products
   public async Task<IEnumerable<Product>> GetProducts(int subcatId)
   {
       // IQueryable<SubCategory> subCategoriesQuery = context.SubCategory.AsQueryable();
       // if (searchParameters.CategoryIdContains != null)
       // {
       //     subCategoriesQuery = subCategoriesQuery.Where(u => u.CategoryId ==searchParameters.CategoryIdContains);
       // }
       // IEnumerable<SubCategory> result = await subCategoriesQuery.ToListAsync();
       // return result;
        
       IQueryable<Product> productsQuery = context.Products.Include (sc => sc.SubCategory).AsQueryable();
       if ( subcatId != 0)
       {
           productsQuery = productsQuery.Where(u => u.SubCategory.Id == subcatId);
       }
   
       List<Product> result = await productsQuery.ToListAsync();
       return result;
   }
}