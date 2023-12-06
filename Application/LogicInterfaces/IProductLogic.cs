using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IProductLogic
{
    Task<Product> CreateAsync(ProductCreationDto dto);
    
    //view post - search/list
    Task<IEnumerable<Product>> GetAsync(SearchProductParametersDto searchParameters);
  
  // view single post ??????
     Task<ProductBasicDto> GetByIdAsync(int id);

     //delete product
     Task DeleteAsync(int id);
     
     //drop down products
     Task<IEnumerable<Product>> GetProducts(int subCategoryId);
}