using System;
using System.Linq;
using System.Runtime.Remoting;

namespace EF_Practices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new EFTestContext();
            var products = context.Customers.ToList();

            context.SaveChanges();

            Console.Read();
        }
    }
}