using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace EF_Practices
{
    public class EFBasic
    {
        /// <summary>
        /// 透過ToList, foreach才會觸發連結DB
        /// </summary>
        public static void AboutDbContext()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var products = from product in context.Products
                    select product;

                var list = products.ToList();
            }
        }

        public static void UpdateEntity()
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

        public static void ReloadEntity()
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

        public static void ObjectContext()
        {
            using (var context = new EFTestContext())
            {
                var objectContextAdapter = (IObjectContextAdapter)context;
                var objectContext = objectContextAdapter.ObjectContext;
                var products = objectContext.CreateObjectSet<TProduct>();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name},{product.Money}");
                }
            }
        }

        public static void SqlQueryByDbSet()
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

        public static void EFInsertAction()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Products.Add(new TProduct { Name = "MVC 開發實戰", Money = 399, Category = "網頁開發" });
                context.Products.AddRange(new List<TProduct>
                {
                    new TProduct {Name = "Docker 入門實戰", Money = 399, Category = "容器"},
                    new TProduct {Name = "大話設計", Money = 399, Category = "設計模式"}
                });

                context.SaveChanges();
            }
        }

        public static void EFDeleteAction()
        {
            using (var context = new EFTestContext())
            {
                // EF Remove, RemoveRange是一筆一筆刪除
                context.Database.Log = Console.WriteLine;
                var product = context.Products.FirstOrDefault();
                context.Products.Remove(product);
                var products = context.Products.Where(p => p.PId > 1);
                context.Products.RemoveRange(products);

                context.SaveChanges();
            }
        }

        public static void EFLocal()
        {
            using (var context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var local = context.Products.Local;
                Console.WriteLine($"Local: {local.Count}");
                foreach (var product in context.Products)
                {

                }

                Console.WriteLine($"Query-Local: {local.Count}");
                context.Products.Add(new TProduct { Name = "大話重購", Money = 399, Category = "重購" });
                Console.WriteLine($"Add-Local: {local.Count}");
                Console.WriteLine($"Really-Local: {context.Products.Count()}");
            }
        }
    }
}