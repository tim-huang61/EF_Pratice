using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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


            EFQueryAndFilter.SelectMany();
            EFQueryAndFilter.OfType();
            EFQueryAndFilter.GroupByCondition();
            EFQueryAndFilter.Lookup();
            EFQueryAndFilter.GetProducts();

            Console.Read();
        }
    }
}