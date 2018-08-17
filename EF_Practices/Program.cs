using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace EF_Practices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // AboutDbContext();
            // UpdateEntity();
            // ReloadEntity();
            // ObjectContext();
            // SqlQueryByDbSet();
            EFInsertAction();

            Console.Read();
        }

        /// <summary>
        /// 透過ToList, foreach才會觸發連結DB
        /// </summary>
        private static void AboutDbContext()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var products = from product in context.Products
                               select product;

                var list = products.ToList();
            }
        }

        private static void UpdateEntity()
        {
            using (var context = new EFTestContext())
            {
                var product = context.Products.FirstOrDefault();
                Console.WriteLine($"Origin Data: {product.Name},{product.Money}");
                product.Money = 299;
                context.SaveChanges();
                Console.WriteLine($"After Update: {product.Name},{product.Money}");
            }
        }

        private static void ReloadEntity()
        {
            using (var context = new EFTestContext())
            {
                var product = context.Products.FirstOrDefault();
                Console.WriteLine($"Origin Data: {product.Name},{product.Money}");
                Console.WriteLine("Press Any Key After Update Database ");
                Console.ReadLine();
                context.Entry(product).Reload();
                Console.WriteLine($"After Reload: {product.Name},{product.Money}");
            }
        }

        private static void ObjectContext()
        {
            using (var context = new EFTestContext())
            {
                var objectContextAdapter = (IObjectContextAdapter)context;
                var objectContext = objectContextAdapter.ObjectContext;
                var products = objectContext.CreateObjectSet<Product>();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name},{product.Money}");
                }
            }
        }

        private static void SqlQueryByDbSet()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var products = context.Products.SqlQuery(@"SELECT PId, ProductName AS Name, Price AS Money, Category, CreateTime
                                                         FROM Products WHERE PId = @id", new SqlParameter("id", 1)).ToList();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name},{product.Money}");
                }
            }
        }

        private static void EFInsertAction()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Products.Add(new Product {Name = "MVC 開發實戰", Money = 399, Category = "網頁開發"});
                context.Products.AddRange(new List<Product>
                {
                    new Product {Name = "Docker 入門實戰", Money = 399, Category = "容器"},
                    new Product {Name = "大話設計", Money = 399, Category = "設計模式"}
                });

                context.SaveChanges();
            }
        }
    }
}