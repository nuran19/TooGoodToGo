using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Application.Logic;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace TestProject;

[TestClass]
public class CategoryLogicTests
{
    [TestMethod]
    public async Task GetAsync_ReturnsCategories()
    {
        // Arrange
        var mockCategoryDao = new Mock<ICategoryDao>();
        mockCategoryDao.Setup(dao => dao.GetAsync()).ReturnsAsync(new List<Category>
        {
            new Category( "Category1"),
            new Category("Category2" )
            // Adjust based on the actual constructor parameters of your Category class
        });


        var categoryLogic = new CategoryLogic(mockCategoryDao.Object);

        // Act
        var result = await categoryLogic.GetAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count()); // Adjust based on the expected number of categories
        // Add more specific assertions based on your logic and expected outcomes
    }
}
