/*using System;
using System.Linq;

namespace CustomORMExample
{
    class Program
    {
        static void Main()
        {
            string connectionString = "DefaultConnection";
            var dbContext = new DbContext(connectionString);

            // Create a repository for the Product entity
            var productRepository = new Repository<Product>(dbContext);

            // Adding a new product
            var newProduct = new Product { Name = "Laptop", Price = 1200.50m };
            productRepository.Add(newProduct);

            // Get all products
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            }

            // Update a product
            var productToUpdate = products.First();
            productToUpdate.Price = 1500;
            productRepository.Update(productToUpdate);

            // Delete a product
            productRepository.Delete(productToUpdate.ProductId);
        }
    }
}
*/

using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CustomORMExample
{
    class Program
    {
        static void Main()
        {
            // Load appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve the connection string from appsettings.json
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Initialize DbContext
            var dbContext = new DbContext(connectionString);

            // Create a repository for the Product entity
            var productRepository = new Repository<Product>(dbContext);

            // Adding a new product
            var newProduct = new Product { Name = "Laptop", Price = 1200.50m };
            productRepository.Add(newProduct);

            // Get all products
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            }
            Console.ReadLine();
            // Update a product
            var productToUpdate = products.ElementAt(0);
            productToUpdate.Price = 1500;
            productRepository.Update(productToUpdate);
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            }
            Console.ReadLine();
            // Delete a product
            /*productRepository.Delete(productToUpdate.ProductId);
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            }
            Console.ReadLine();*/
        }
    }
}
