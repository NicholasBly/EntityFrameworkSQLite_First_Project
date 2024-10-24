using System;
using System.Linq;

namespace EFSQLiteSample
{
    public delegate void LogDelegate(string message);

    class Program
    {
        static void Main(string[] args)
        {
            LogDelegate logDel = LogToConsole;

            logDel += LogToFile;

            using (var context = new AppDbContext())
            {
                // Create product
                var product = new Product { Name = "Laptop", Price = 1200.00M };
                context.Products.Add(product);
                context.SaveChanges();
                logDel?.Invoke($"Created Product: {product.Name} with Price: {product.Price}");

                // Read products
                var products = context.Products.ToList();
                logDel?.Invoke("Reading products from database:");
                foreach (var p in products)
                {
                    logDel?.Invoke($"- {p.Name} - ${p.Price}");
                }

                // Update the first product
                var firstProduct = context.Products.First();
                firstProduct.Price = 1300.00M;
                context.SaveChanges();
                logDel?.Invoke($"Updated Product: {firstProduct.Name} to new Price: {firstProduct.Price}");

                // Delete the product
                context.Products.Remove(firstProduct);
                context.SaveChanges();
                logDel?.Invoke($"Deleted Product: {firstProduct.Name}");
            }
        }

        public static void LogToConsole(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }

        public static void LogToFile(string message)
        {
            using (var writer = new System.IO.StreamWriter("log.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
