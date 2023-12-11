using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject;

[TestClass]
    public class UserDao_CreateAsync_Test
    {
        [TestMethod]
        public async Task CreateUser_ValidInput_ReturnsCreatedUser()
        {
            // Arrange
            var mockUserDao = new Mock<IUserDao>();

            // Mock the CreateAsync method to return a user
            mockUserDao.Setup(dao => dao.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) => user);

            var userLogic = new UserLogic(mockUserDao.Object);

            // Create a sample UserCreationDto
            var userCreationDto = new UserCreationDto
            {
                UserName = "testuser",
                Password = "password",
                CompanyId = 1,
                Role = "Employee"
            };

            // Act
            var createdUser = await userLogic.CreateAsync(userCreationDto);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.AreEqual("testuser", createdUser.UserName);
            // Add more assertions based on your specific User properties

            // Verify that CreateAsync was called with the correct user
            mockUserDao.Verify(dao => dao.CreateAsync(It.IsAny<User>()), Times.Once);
        }
    }
    
    