using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DO;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

static internal class DataSource
{
    static readonly int RandNum=0;
    internal static Order[] arrayOrder=new Order[100];
    internal static OrderItem[] arrayOrderItem = new OrderItem[200];
    internal static Product[] arrayProduct = new Product[50];

    internal static string[] ProductName = { "Lamborgini", "BMW", "Ferrari","Porche","Jeep","Tesla","jaguar","Audi","Corvette","MINI cooper" };
    internal static string[] CustomerName = {"Moshe","Yeoram","Yossi","Dani","Avi","Sapir","Hadar"}
    //internal static string[] CategoryName = { "Familly car", "Race car", "4x4", "Sport" };
    private static void _InitializeProduct()
    {
        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            p.ID = rand.Next(100000, 1000000);
            p.Name = ProductName[rand.Next(0, 10)];
            var v = Enum.GetValues(typeof(Category));
            p.ProductCategory = (Category)v.GetValue(rand.Next(0, 10));
            p.InStock = rand.Next(1, 5);
            p.Price = rand.Next(200000, 5000000);
        }
    }
    private static void _InitializeOrder()
    {
        Random rand = new Random();
        for (int i = 0; i < 20; i++)
        {
            Order _order = new Order();
            _order.ID = rand.Next(100000, 1000000);
            _order.CustomerName = CustomerName[rand.Next(0, 10)];
            _order.DateCreateDelivery = DateTime.MinValue;

            var v = Enum.GetValues(typeof(Category));
            p.ProductCategory = (Category)v.GetValue(rand.Next(0, 10));
            p.InStock = rand.Next(1, 5);
            p.Price = rand.Next(200000, 5000000);
        }
    }
    static DataSource()
    {
       s_Initialize();
    }

    private static void s_Initialize()
    {
        

    }
}
