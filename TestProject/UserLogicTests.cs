using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;

namespace TestProject;

[TestClass]
public class UserLogicTests
{
    [TestMethod]
    public async Task CreateUser_ValidInput_Success()
    {
        // Arrange
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        var userLogic = new UserLogic(userDaoMock.Object);
        var userCreationDto = new UserCreationDto
        {
            UserName = "NewUser",
            Password = "SecurePass",
            CompanyId = 1,
            Role = "Employee"
        };

        // Act
        var createdUser = await userLogic.CreateAsync(userCreationDto);

        // Assert
        Assert.IsNotNull(createdUser);
        Assert.AreEqual(userCreationDto.UserName, createdUser.UserName);
        // Add more assertions based on your specific logic
    }
}
