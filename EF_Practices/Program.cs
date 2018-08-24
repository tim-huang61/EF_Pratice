using EF_Practices.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EF_Practices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // EFBasic.AboutDbContext();
            // EFBasic.UpdateEntity();
            // EFBasic.ReloadEntity();
            // EFBasic.ObjectContext();
            // EFBasic.SqlQueryByDbSet();
            // EFBasic.EFInsertAction();
            // EFBasic.EFDeleteAction();
            // EFBasic.EFLocal();

            // EFQueryAndFilter.SelectMany();
            // EFQueryAndFilter.OfType();
            // EFQueryAndFilter.GroupByCondition();
            // EFQueryAndFilter.Lookup();
            // EFQueryAndFilter.GetProducts();
            // EFQueryAndFilter.JoinGroup();

            OneToMany();
            OneToManyCutomFK();
            OneToOne();

            Console.Read();
        }

        private static void OneToOne()
        {
            using (EFTestContext context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var product = context.Products.Include(p => p.Book).First();
                product.Book = new TBook { Page = 300 };
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void OneToManyCutomFK()
        {
            using (EFTestContext context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var userRole = context.UserRoles.First();
                context.Users.Add(new TUser
                {
                    Name = "Wendy",
                    Account = "Wendy",
                    Password = "123456",
                    UserRole = userRole
                });

                context.SaveChanges();
            }
        }

        private static void OneToMany()
        {
            using (EFTestContext context = new EFTestContext())
            {
                context.Database.Log = Console.WriteLine;
                var order = new TOrder
                {
                    OrderDate = DateTime.Now,
                    OrderDetails = new List<TOrderDetail>
                    {
                        new TOrderDetail {Price = 699, ProductName = "Design Pattern", Quantity = 1},
                        new TOrderDetail {Price = 499, ProductName = "MVC In Action", Quantity = 1}
                    }
                };

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
}