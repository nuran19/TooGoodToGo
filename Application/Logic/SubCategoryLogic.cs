using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SubCategoryLogic:ISubCategoryLogic
{
    private readonly ISubCategoryDao subCatDao;
    

    public SubCategoryLogic(ISubCategoryDao subCatDao)  //User DAO is received through constructor dependency injection
    {
        this.subCatDao = subCatDao;
    }
    
    public Task<IEnumerable<SubCategory>> GetAsync(int categoryId )
    {
        return subCatDao.GetAsync(categoryId);
    }
}
