using System;
using System.Collections;
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

            SelectMany();
            OfType();
            GroupByCondition();

            Console.Read();
        }

        private static void SelectMany()
        {
            var context = new NorthwindContext();
            context.Database.Log = Console.WriteLine;
            var list = context.Customers.Where(c => c.CompanyName == "Alfreds Futterkiste")
                .SelectMany(c => c.Orders.Select(o => new {o.CustomerID, o.OrderID})).ToList();

            Console.WriteLine($"Count: {list.Count}");
        }

        private static void OfType()
        {
            var arrayList = new ArrayList {"Tim", 61};
            foreach (var s in arrayList.OfType<string>())
            {
                Console.WriteLine(s);
            }
        }

        private static void GroupByCondition()
        {
            var months = Enumerable.Range(1, 12);
            foreach (var item in months.GroupBy(m => DateTime.DaysInMonth(2018, m)).OrderByDescending(g => g.Key))
            {
                Console.WriteLine($"Days: {item.Key}, Months: {string.Join(",", item)}");
            }

            var enumerable = from month in months
                group month by DateTime.DaysInMonth(2018, month)
                into x
                orderby x.Key descending
                select string.Join(",", x);

            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }
        }
    }
}