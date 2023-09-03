using Microsoft.EntityFrameworkCore;
using Practice_Api.Data;
using Practice_Api.Models;

namespace Practice_Api_TestProject.Helpers
{
    public class InMemoryDbContextFixture : IDisposable
    {
        public AppDbContext DbContext { get; }

        public InMemoryDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new AppDbContext(options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0m },
                new Product { Id = 2, Name = "Product 2", Price = 20.0m }
            };

            DbContext.Products.AddRange(products);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}