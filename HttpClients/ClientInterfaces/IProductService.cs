using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IProductService
{
    //to create product
    Task<int> CreateAsync( ProductCreationDto dto); 
    
    //view all products
 //   Task<ICollection<string>> GetAsync(string? titleContains)
 Task<ICollection<Product>> GetAsync( 
     string? userName, 
     int? userId, 
     int? companyId,
     string? subCategory,
     string? typeContains,
     string? brandContains
 );
 
 //get product by id 
 Task<ProductBasicDto> GetByIdAsync(int id);
 
 //delete product
 Task DeleteAsync(int id);
 
 Task<IEnumerable<Product>> GetProducts(int subCatId);
}