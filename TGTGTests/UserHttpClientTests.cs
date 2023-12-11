using System;
using System.Net;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

[TestClass]
public class UserHttpClientTests
{
    private UserHttpClient userHttpClient;
    private Mock<HttpMessageHandler> handlerMock;
    private HttpClient httpClient;

    [TestInitialize]
    public void Initialize()
    {
        handlerMock = new Mock<HttpMessageHandler>();
        httpClient = new HttpClient(handlerMock.Object);
        userHttpClient = new UserHttpClient(httpClient);
    }

    [TestMethod]
    public async Task Create_ValidUser_ReturnsUser()
    {
        // Arrange
        var expectedUser = new User { Id = 15, UserName = "xpuya", CompanyId = 1, Role = "Employee" };
        var userCreationDto = new UserCreationDto("xpuya", "12345678", 1, "Employee");

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(expectedUser, typeof(User), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), Encoding.UTF8, "application/json")
        };

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var result = await userHttpClient.Create(userCreationDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedUser.Id, result.Id);
        Assert.AreEqual(expectedUser.UserName, result.UserName);
        Assert.AreEqual(expectedUser.CompanyId, result.CompanyId);
        Assert.AreEqual(expectedUser.Role, result.Role);
    }

}