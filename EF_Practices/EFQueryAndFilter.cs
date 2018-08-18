using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EF_Practices;

static internal class EFQueryAndFilter
{
    public static void GetProducts()
    {
        ProductAsync();
        Console.WriteLine("Data Loading...");
    }

    private static async Task ProductAsync()
    {
        var context = new NorthwindContext();
        var listAsync = await context.Products.ToListAsync();
        foreach (var product in listAsync)
        {
            Console.WriteLine(product.ProductName);
        }
    }

    public static void Lookup()
    {
        // 針對category去多次做select ex: category有1,2,3 就會select三次
        var context = new NorthwindContext();
        context.Database.Log = Console.WriteLine;
        var lookup = context.Products.ToLookup(p => p.Category);
    }

    public static void SelectMany()
    {
        var context = new NorthwindContext();
        context.Database.Log = Console.WriteLine;
        var list = context.Customers.Where(c => c.CompanyName == "Alfreds Futterkiste")
            .SelectMany(c => c.Orders.Select(o => new {o.CustomerID, o.OrderID})).ToList();

        Console.WriteLine($"Count: {list.Count}");
    }

    public static void OfType()
    {
        var arrayList = new ArrayList {"Tim", 61};
        foreach (var s in arrayList.OfType<string>())
        {
            Console.WriteLine(s);
        }
    }

    public static void GroupByCondition()
    {
        var months = Enumerable.Range(1, 12);
        foreach (var item in months.GroupBy(m => DateTime.DaysInMonth(2018, m)).OrderByDescending(g => g.Key))
        {
            Console.WriteLine($"Days: {item.Key}, Months: {String.Join(",", item)}");
        }

        var enumerable = from month in months
            group month by DateTime.DaysInMonth(2018, month)
            into x
            orderby x.Key descending
            select String.Join(",", x);

        foreach (var item in enumerable)
        {
            Console.WriteLine(item);
        }
    }
}