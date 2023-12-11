using Domain.DTOs;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TGTGTests;

[TestClass]
public class DayContentCreationDtoTests
{
    [TestMethod]
    public void DayContentCreationDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        int ownerId = 1;
        DateOnly date = new DateOnly(2023, 12, 11);
        ICollection<DayContentProduct> dayContentProducts = new List<DayContentProduct>();

        // Act
        var dayContentCreationDto = new DayContentCreationDto(ownerId, date, dayContentProducts);

        // Assert
        Assert.AreEqual(ownerId, dayContentCreationDto.OwnerId);
        Assert.AreEqual(date, dayContentCreationDto.Date);
        Assert.AreEqual(dayContentProducts, dayContentCreationDto.DayContentProducts);
    }
}

[TestClass]
public class ProductBasicDtoTests
{
    [TestMethod]
    public void ProductBasicDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        int id = 1;
        string ownerName = "John";
        string categoryName = "Fruits";
        string subCategoryName = "Exotic Fruits";
        string type = "Dragon fruit";
        string brand = "Brand X";
        double qty = 105;

        // Act
        var productBasicDto = new ProductBasicDto(id, ownerName, categoryName, subCategoryName, type, brand, qty);

        // Assert
        Assert.AreEqual(id, productBasicDto.Id);
        Assert.AreEqual(ownerName, productBasicDto.OwnerName);
        Assert.AreEqual(categoryName, productBasicDto.CategoryName);
        Assert.AreEqual(subCategoryName, productBasicDto.SubCategoryName);
        Assert.AreEqual(type, productBasicDto.Type);
        Assert.AreEqual(brand, productBasicDto.Brand);
        Assert.AreEqual(qty, productBasicDto.Qty);
    }
}
[TestClass]
public class SearchChartParametersDtoTests
{
    [TestMethod]
    public void SearchChartParametersDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        int companyId = 1;
        string productTypeContains = "Banana";

        // Act
        var searchChartParametersDto = new SearchChartParametersDto(companyId, productTypeContains);

        // Assert
        Assert.AreEqual(companyId, searchChartParametersDto.CompanyId);
        Assert.AreEqual(productTypeContains, searchChartParametersDto.ProductTypeContains);
    }
}

[TestClass]
public class DayContentProductUpdateDtoTests
{
    [TestMethod]
    public void DayContentProductUpdateDto_Properties_ShouldSetCorrectly()
    {
        // Arrange
        int productId = 1;
        int quantity = 5;
        string reason = "Restock";

        // Act
        var productUpdateDto = new DayContentProductUpdateDto
        {
            ProductId = productId,
            Quantity = quantity,
            Reason = reason
        };

        // Assert
        Assert.AreEqual(productId, productUpdateDto.ProductId);
        Assert.AreEqual(quantity, productUpdateDto.Quantity);
        Assert.AreEqual(reason, productUpdateDto.Reason);
    }
}
[TestClass]
public class SearchDayContentParametersDtoTests
{
    [TestMethod]
    public void SearchDayContentParametersDto_Properties_ShouldSetCorrectly()
    {
        // Arrange
        string username = "user1";
        int userId = 1;
        DateOnly date = new DateOnly(2023, 12, 11);
        int productId = 5;

        // Act
        var searchDto = new SearchDayContentParametersDto(username, userId, date, productId);

        // Assert
        Assert.AreEqual(username, searchDto.Username);
        Assert.AreEqual(userId, searchDto.UserId);
        Assert.AreEqual(date, searchDto.Date);
        Assert.AreEqual(productId, searchDto.ProductId);
    }
}

[TestClass]
public class SearchProductParametersDtoTests
{
    [TestMethod]
    public void SearchProductParametersDto_Properties_ShouldSetCorrectly()
    {
        // Arrange
        string username = "alex";
        int userId = 1;
        int companyId = 2;
        string subCategoryContains = "Deli meats";
        string typeContains = "Salami";
        string brandContains = "Delimeats";
        int dayContentId = 3;

        // Act
        var searchDto = new SearchProductParametersDto(username, userId, companyId, subCategoryContains, typeContains, brandContains, dayContentId);

        // Assert
        Assert.AreEqual(username, searchDto.Username);
        Assert.AreEqual(userId, searchDto.UserId);
        Assert.AreEqual(companyId, searchDto.CompanyId);
        Assert.AreEqual(subCategoryContains, searchDto.SubCategoryContains);
        Assert.AreEqual(typeContains, searchDto.TypeContains);
        Assert.AreEqual(brandContains, searchDto.BrandContains);
        Assert.AreEqual(dayContentId, searchDto.DayContentId);
    }
}

[TestClass]
public class SearchUserParametersDtoTests
{
    [TestMethod]
    public void SearchUserParametersDto_Properties_ShouldSetCorrectly()
    {
        // Arrange
        string usernameContains = "john";
        int userIdContains = 1;
        int companyIdContains = 2;
        string roleContains = "admin";

        // Act
        var searchDto = new SearchUserParametersDto(usernameContains, userIdContains, companyIdContains, roleContains);

        // Assert
        Assert.AreEqual(usernameContains, searchDto.UsernameContains);
        Assert.AreEqual(userIdContains, searchDto.UserIdContains);
        Assert.AreEqual(companyIdContains, searchDto.CompanyIdContains);
        Assert.AreEqual(roleContains, searchDto.RoleContains);
    }
}

[TestClass]
public class UserCreationDtoTests
{
    [TestMethod]
    public void UserCreationDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        string userName = "user1";
        string password = "secure_password";
        int companyId = 1;
        string role = "user";

        // Act
        var userCreationDto = new UserCreationDto(userName, password, companyId, role);

        // Assert
        Assert.AreEqual(userName, userCreationDto.UserName);
        Assert.AreEqual(password, userCreationDto.Password);
        Assert.AreEqual(companyId, userCreationDto.CompanyId);
        Assert.AreEqual(role, userCreationDto.Role);
    }
}

[TestClass]
public class UserLoginDtoTests
{
    [TestMethod]
    public void UserLoginDto_Properties_ShouldSetCorrectly()
    {
        // Arrange
        string username = "john_doe";
        string password = "secure_password";

        // Act
        var loginDto = new UserLoginDto { Username = username, Password = password };

        // Assert
        Assert.AreEqual(username, loginDto.Username);
        Assert.AreEqual(password, loginDto.Password);
    }
}
