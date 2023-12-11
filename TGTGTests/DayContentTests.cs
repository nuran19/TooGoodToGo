using Domain.DTOs;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TGTGTests;

    [TestClass]
    public class DayContentTests
    {
        [TestMethod]
        public void DayContentCreationDto_ConstructedCorrectly()
        {
            // Arrange
            int ownerId = 1;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            ICollection<DayContentProduct> dayContentProducts = new List<DayContentProduct>
            {
                new DayContentProduct(1, 1, 10, "Reason1"),
                new DayContentProduct(2, 2, 20, "Reason2")
            };

            // Act
            var dayContentCreationDto = new DayContentCreationDto(ownerId, date, dayContentProducts);

            // Assert
            Assert.AreEqual(ownerId, dayContentCreationDto.OwnerId);
            Assert.AreEqual(date, dayContentCreationDto.Date);

            // Use a loop to compare each element
            Assert.AreEqual(dayContentProducts.Count, dayContentCreationDto.DayContentProducts.Count);
            for (int i = 0; i < dayContentProducts.Count; i++)
            {
                Assert.AreEqual(dayContentProducts.ElementAt(i), dayContentCreationDto.DayContentProducts.ElementAt(i));
            }
        }


        [TestMethod]
        public void DayContentBasicDto_ConstructedCorrectly()
        {
            // Arrange
            int id = 1;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var dayContentBasicDto = new DayContentBasicDto(id, date);

            // Assert
            Assert.AreEqual(id, dayContentBasicDto.Id);
            Assert.AreEqual(date, dayContentBasicDto.Date);
        }

        [TestMethod]
        public void DayContentProductUpdateDto_ConstructedCorrectly()
        {
            // Arrange
            int productId = 1;
            int quantity = 10;
            string reason = "Some reason";

            // Act
            var dayContentProductUpdateDto = new DayContentProductUpdateDto
            {
                ProductId = productId,
                Quantity = quantity,
                Reason = reason
            };

            // Assert
            Assert.AreEqual(productId, dayContentProductUpdateDto.ProductId);
            Assert.AreEqual(quantity, dayContentProductUpdateDto.Quantity);
            Assert.AreEqual(reason, dayContentProductUpdateDto.Reason);
        }
    }

    [TestClass]
    public class DayContentProductTests
    {
        [TestMethod]
        public void DayContentProduct_ConstructedCorrectly()
        {
            // Arrange
            int dayContentId = 1;
            int productId = 2;
            int quantity = 10;
            string reason = "Some reason";

            // Act
            var dayContentProduct = new DayContentProduct(dayContentId, productId, quantity, reason);

            // Assert
            Assert.AreEqual(dayContentId, dayContentProduct.DayContentId);
            Assert.AreEqual(productId, dayContentProduct.ProductId);
            Assert.AreEqual(quantity, dayContentProduct.Quantity);
            Assert.AreEqual(reason, dayContentProduct.Reason);
        }
    }

    [TestClass]
    public class DayContentProductDetailTests
    {
        [TestMethod]
        public void DayContentProductDetail_ConstructedCorrectly()
        {
            // Arrange
            int dayContentId = 1;
            int productId = 2;
            string productName = "Product1";
            string reason = "Some reason";
            int quantity = 10;

            // Act
            var dayContentProductDetail = new DayContentProductDetail(dayContentId, productId, productName, reason, quantity);

            // Assert
            Assert.AreEqual(dayContentId, dayContentProductDetail.DayContentId);
            Assert.AreEqual(productId, dayContentProductDetail.ProductId);
            Assert.AreEqual(productName, dayContentProductDetail.ProductName);
            Assert.AreEqual(reason, dayContentProductDetail.Reason);
            Assert.AreEqual(quantity, dayContentProductDetail.Quantity);
        }
    }


