using Microsoft.AspNetCore.Mvc;
using MyWebApi.Controllers;
using Practice_Api.Data;
using Practice_Api.Models;
using Practice_Api_TestProject.Helpers;

namespace Practice_Api_TestProject
{
    public class ProductControllerTests : IClassFixture<InMemoryDbContextFixture>
    {
        private readonly InMemoryDbContextFixture _fixture;
        private readonly ProductController _controller;

        public ProductControllerTests(InMemoryDbContextFixture fixture)
        {
            _fixture = fixture;
            _controller = new ProductController(new ProductRepository(_fixture.DbContext));
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, actualProducts.Count); // Assuming 2 products from seed
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenValidInput()
        {
            // Arrange
            var productId = 10;
            var updatedProduct = new Product { Id = productId, Name = "Updated Product", Price = 25.0m };

            // Act
            var result = await _controller.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsType<NoContentResult>(result);
            // You might want to retrieve the product from the database to ensure it's updated as expected
        }        


        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenFound()
        {
            // Arrange
            var productId = 10;
            var expectedProduct = new Product { Id = productId, Name = "Product 1", Price = 10.0m };

            _fixture.DbContext.Products.Add(expectedProduct);
            _fixture.DbContext.SaveChanges();

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            var productId = 100; // Assuming no product with this ID exists in the database

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenValidInput()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            // You might want to ensure that the product is actually deleted from the database
        }
    }
}