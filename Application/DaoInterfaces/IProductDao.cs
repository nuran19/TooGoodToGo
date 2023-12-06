using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IProductDao
{
    Task<Product> CreateAsync(Product product);  //The method takes a product and returns a product (because the Id is set).
    
    //view products - search
    Task<IEnumerable<Product>> GetAsync(SearchProductParametersDto searchParameters);
    
    //view single product  
    Task <Product?> GetByIdAsync(int id);
  
    //delete product
    Task DeleteAsync(int id);
    
    //drop down product
    Task<IEnumerable<Product>> GetProducts(int subCategoryId);
}