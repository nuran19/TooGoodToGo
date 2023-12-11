using Domain.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TGTGTests;

[TestClass]
public class DayContentBasicDtoTests
{
    [TestMethod]
    public void DayContentBasicDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        int id = 1;
        DateOnly date = new DateOnly(2023, 12, 11);

        // Act
        var dayContentBasicDto = new DayContentBasicDto(id, date);

        // Assert
        Assert.AreEqual(id, dayContentBasicDto.Id);
        Assert.AreEqual(date, dayContentBasicDto.Date);
    }
}