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

    static readonly int RandNum = 0;
    internal static Order[] arrayOrder = new Order[100];
    internal static OrderItem[] arrayOrderItem = new OrderItem[200];
    internal static Product[] arrayProduct = new Product[50];

    internal static string[] ProductName = { "Lamborgini", "BMW", "Ferrari", "Porche", "Jeep", "Tesla", "jaguar", "Audi", "Corvette", "MINI cooper" };
    internal static string[] CustomerName = { "Moshe", "Yeoram", "Yossi", "Dani", "Avi", "Sapir", "Hadar" };

    static internal class _Config
    {
        // how much places are contain thigs
        internal static int _ProductIndex = 0;
        internal static int _OrderIndex = 0;
        internal static int _OrderItemIndex = 0;

        //
        private static int _OrderID = 10000;
        public static int get_OrderID
        {
            get
            { return _OrderID++; }
        }

        private static int _OrderItemID = 10000;
        public static int get_OrderItemID
        {
            get
            { return _OrderItemID++; }
        }
        private static int _ProductID = 10000;
        public static int get_ProductID
        {
            get
            {
                Random randi = new Random();
                _ProductID = 100000 + _ProductIndex * 20 + _ProductIndex * 3;
                return _ProductID;
            }
        }
    }


    private static void _InitializeProduct()//initializ product
    {
        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            p.ID = _Config.get_ProductID;// i * 100000 + i * 20 + i * 3;//create a uniqe ID for each priduct
            p.Name = ProductName[rand.Next(0, 10)];
            var v = Enum.GetValues(typeof(Category));
            p.ProductCategory = (Category)v.GetValue(rand.Next(0, 4));
            p.InStock = rand.Next(1, 5);
            p.Price = rand.Next(200000, 5000000);
        }
    }
    private static void _InitializeOrder()//initializ order
    {
        Random rand = new Random();
        for (int i = 0; i < 20; i++)
        {
            _Config._OrderIndex++;
            Order _order = new Order();
            _order.ID = _Config.get_OrderID;
            _order.CustomerName = CustomerName[rand.Next(0, 7)];
            _order.DateOfOrder = DateTime.MinValue;
            TimeSpan spaceTime = TimeSpan.FromDays(1);
            _order.DateCreateDelivery = _order.DateOfOrder + spaceTime;//one day after the order the dilevery will create
            _order.DateOfDelivery = _order.DateCreateDelivery + spaceTime;//one day after the order the dilevery will dlivered
            _order.CustamerAddress = "jrusalem";
            _order.CustomerMail = "maill@gmail.com";

        }
    }

    private static void _InitializeOrdetItem()//initializ orderItem
    {
        Random rand = new Random();
        for (int i = 0; i < 40; i++)
        {
            _Config._OrderItemIndex++;
            OrderItem _orderItem = new OrderItem();
            _orderItem.ID = _Config.get_OrderItemID;
            _orderItem.NumOfOrder = arrayOrder[i % 19].ID;//i%19 will chosse the order , in this way all the place be uesed
            int index = rand.Next(0, 10);//choss a product 
            _orderItem.IdOfItem = arrayProduct[index].ID;
            _orderItem.amount = rand.Next(1, 5);
            _orderItem.Price = arrayProduct[index].Price;

        }

    }
    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        _InitializeProduct();
        _InitializeOrder();
        _InitializeOrdetItem();
    }
}