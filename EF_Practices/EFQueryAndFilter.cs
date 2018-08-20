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

    public static void JoinGroup()
    {
        var context = new NorthwindContext();
        context.Database.Log = Console.WriteLine;
        var item1 = context.Customers.GroupJoin(context.Orders, c => c.CustomerID, o => o.CustomerID, (c, o) => new
        {
            Customer = c,
            Orders = o
        }).ToList();

        var item2 = context.Customers.GroupJoin(context.Orders, c => c.CustomerID, o => o.CustomerID, (c, o) => new
        {
            Customer = c,
            Orders = o
        }).SelectMany(item => item.Orders.DefaultIfEmpty(), (item, order) =>
            new
            {
                CustomerID = item.Customer.CustomerID,
                OrderID = order == null ? 0 : order.OrderID
            }
        ).ToList();

        var item3 = (from customer in context.Customers
            join order in context.Orders
                on customer.CustomerID equals order.CustomerID into os
            select new
            {
                Customer = customer,
                Orders = os
            }).ToList();

        var item4 = (from customer in context.Customers
            join order in context.Orders
                on customer.CustomerID equals order.CustomerID into os
            from o in os.DefaultIfEmpty()
            select new
            {
                CustomerID = customer.CustomerID,
                OrderID = o == null ? 0 : o.OrderID
            }).ToList();
    }
}