using System.ComponentModel.Design;
using System.Runtime.InteropServices.JavaScript;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.Logic;

public class DayContentLogic : IDayContentLogic
{
    private readonly IDayContentDao dayContentDao;
    private readonly IDayContentProductDao dayContentProductDao;
    private readonly IUserDao userDao;

    public DayContentLogic(IDayContentDao dayContentDao, IUserDao userDao, IDayContentProductDao dayContentProductDao)
    {
        this.dayContentDao = dayContentDao;
        this.userDao = userDao;
        this.dayContentProductDao = dayContentProductDao;

    }

    public async Task<DayContent> CreateAsync(DayContentCreationDto dto)
    {
         
        DayContent dayContent = new DayContent(dto.OwnerId, dto.Date, dto.DayContentProducts);
        DayContent created = await dayContentDao.CreateAsync(dayContent);
        
        // Once the day content is created, then create its details using its generated id
        
        await dayContentProductDao.CreateAsync(created.Id,dto.DayContentProducts.ToList());
        
        return created;
    }

    private void ValidateDayContent(DayContentCreationDto dto)
    {
        if (dto.Date == default(DateOnly)) throw new Exception("Date can not be empty!");
        // other validation stuff
    }
    //View dayContents
    public Task<IEnumerable<DayContent>> GetAsync(SearchDayContentParametersDto searchParameters)
    {
        return dayContentDao.GetAsync(searchParameters);
    }

    public async Task<DayContentBasicDto> GetByIdAsync(int id)
    {
        DayContent? dayContent = await dayContentDao.GetByIdAsync(id);
        if (dayContent == null)
        {
            throw new Exception($"DayContent with id {id} not found.");
        }
        
        return new DayContentBasicDto(dayContent.Id, dayContent.Date);

    }
    
    public async Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId)
    {
        try
        {
            var dayContentProducts = await dayContentProductDao.GetProductsAsync(dayContentId);

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
            //  Logic to retrieve day content products based on dayContentId
            var dayContentProducts = await dayContentProductDao.GetDayContentDetailAsync(dayContentId);

            return dayContentProducts;
        }
        catch (Exception e)
        { 
            Console.WriteLine(e); 
        }

        return null;
    }
    
    public DayContentLogic(IDayContentDao dayContentDao)
    {
        this.dayContentDao = dayContentDao;
    }

    public async Task<List<DayContent>> GetMonthEntries(int month, int year)
    {
        return await dayContentDao.GetMonthEntries(month, year);
    }
    
    public async Task<List<DayContent>> GetEntriesForDateRange(DateOnly startDate, DateOnly endDate)
    {
        return await dayContentDao.GetEntriesForDateRange(startDate, endDate);
    }
    // not in use, day content cannot be updated
    public async Task<DayContent> UpdateAsync(DayContentUpdateDto dto)
    {
        DayContent? existingDayContent = await dayContentDao.GetByIdAsync(dto.Id);
        
        await UpdateRelatedProducts(dto.Id, dto.Products);

        return existingDayContent;
    }

    public async Task UpdateRelatedProducts(int dayContentId, List<DayContentProductUpdateDto> productDtos)
    {
        //DayContent? existingDayContent = await dayContentDao.GetByIdAsync(dayContentId);
        var existingProducts = await dayContentProductDao.GetProductsAsync(dayContentId);
        Console.WriteLine("inside DCLogic " + productDtos.Count);
        // Update existing products based on DTO
        foreach (var productDto in productDtos)
        {
            Console.WriteLine("inside loop "+ productDto.ProductId + "q="+productDto.Quantity+" REason:"+productDto.Reason);
            var existingProduct = existingProducts.FirstOrDefault(p => p.ProductId == productDto.ProductId);

            if (existingProduct != null)
            {
                Console.WriteLine("inside if ");
                // Update properties of existing product
                existingProduct.Quantity = productDto.Quantity;
                existingProduct.Reason = productDto.Reason;
                await dayContentProductDao.UpdateAsync(existingProduct);
            }
            
        }
    }

    public async Task DeleteAsync(int dayContentId, bool deleteDayContent = false, List<int> productsToDelete = null)
        {
            DayContent? dayContent = await dayContentDao.GetByIdAsync(dayContentId);

            if (dayContent == null)
            {
                throw new Exception($"DayContent with ID {dayContentId} not found.");
            } 
            
            if (deleteDayContent) // true means delete everything
            {
                // Load and delete all product from day content
                ICollection<DayContentProduct>? dayContentproducts = await dayContentProductDao.GetProductsAsync(dayContentId);
                foreach (var dcProduct in dayContentproducts)
                { 
                    await dayContentProductDao.DeleteAsync(dcProduct);
                }
                // Delete the entire day content
                await dayContentDao.DeleteAsync(dayContentId);
            } 
            // Delete specific products associated with the day content and keep day content
            else if (productsToDelete.Any())
            {
                foreach (int productId in productsToDelete)
                {
                    DayContentProduct? dayContentproduct = await dayContentProductDao.GetByIdAsync(dayContentId, productId);
                    await dayContentProductDao.DeleteAsync(dayContentproduct);
                }
            }
        }
    }
                
            

            
        
    
