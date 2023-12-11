using Domain.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TGTGTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void UserCreationDto_Initialization_Success()
    {
        // Arrange
        string userName = "TestUser";
        string password = "TestPassword";
        int companyId = 1;
        string role = "Manager";

        // Act
        var userCreationDto = new UserCreationDto(userName, password, companyId, role);

        // Assert
        Assert.AreEqual(userName, userCreationDto.UserName);
        Assert.AreEqual(password, userCreationDto.Password);
        Assert.AreEqual(companyId, userCreationDto.CompanyId);
        Assert.AreEqual(role, userCreationDto.Role);
    }
    }


