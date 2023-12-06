using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ProductLogic : IProductLogic
{
    private readonly IProductDao productDao;
    private readonly IUserDao userDao;

    public ProductLogic(IProductDao productDao, IUserDao userDao)
    {
        this.productDao = productDao;
        this.userDao = userDao;
    }

    public async Task<Product> CreateAsync(ProductCreationDto dto)  
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        
        ValidatePost(dto);
   //     Product product = new Product(user.Id, user.CompanyId, dto.SubCategoryId, dto.Type, dto.Brand, dto.Qty);
   Product product = new Product(user.Id, user.CompanyId, dto.SubCategoryId , dto.Type, dto.Brand, dto.Qty);
        Product created = await productDao.CreateAsync(product);
        return created;
    }

    private void ValidatePost(ProductCreationDto dto)
    {
        if((int)dto.SubCategoryId==0)   throw new Exception($"Product subcategory was not found.");
      // if (string.IsNullOrEmpty(dto.SubCategoryName)) throw new Exception("Product subcategory cannot be empty.");
        if (string.IsNullOrEmpty(dto.Type)) throw new Exception("Product Type cannot be empty.");
        if (string.IsNullOrEmpty(dto.Brand)) throw new Exception("Product Brand cannot be empty.");
        if ((int)(dto.Qty)==0) throw new Exception("Product quantity cannot be empty.");  //?????????????????????????????????????????
        // other validation stuff
    }
    
    //view products  
    public Task<IEnumerable<Product>> GetAsync(SearchProductParametersDto searchParameters)
    {
        return productDao.GetAsync(searchParameters); //returns a Task, but we don't need to await it, because we do not need the result here. Instead, we actually just returns that task, to be awaited somewhere else.
    }

    //view single product  ??????
    public async Task<ProductBasicDto> GetByIdAsync(int id)
    {
        Product? product = await productDao.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception($"Product with id {id} not found");
        }

        return new ProductBasicDto(product.Id, product.Owner.UserName, product.SubCategory.Category.Name, product.SubCategory.Name, product.Type, product.Brand, product.Qty );
        //todo product.SubCategory.Category.Name 
    }

    //delete
    public async Task DeleteAsync(int id)
    {
        Product? product = await productDao.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception($"Product with ID {id} was not found!");
        }
        await productDao.DeleteAsync(id);
    }
  
    //drop down
    public Task<IEnumerable<Product>> GetProducts(int subCategoryId)
    {
        return productDao.GetProducts(subCategoryId);
    }
}