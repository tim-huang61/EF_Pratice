using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;

namespace EF_Practices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new EFTestContext();
            context.Database.Log = Console.WriteLine;
            IEnumerable<Product> products = context.Products.Where(p => p.PId == 1).Where(p=>string.IsNullOrEmpty(p.Name));
            var list = products.ToList();

            Console.Read();
        }
    }
}