﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DO;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

 internal static class DataSource
{

    //static readonly int RandNum = 0;
    internal static List<OrderItem?> listOrderItem = new List<OrderItem?>();
    internal static List<Product?> listProduct = new List<Product?>();
    internal static List<Order?> listOrder = new List<Order?>();



    internal static string[] ProductName = { "Lamborgini", "BMW", "Ferrari", "Porche", "Jeep", "Tesla", "jaguar", "Audi", "Corvette", "MINI cooper" };
    internal static string[] CustomerName = { "Moshe", "Yeoram", "Yossi", "Dani", "Avi", "Sapir", "Hadar" };
    internal static string[] Price = { "100000", "500000", "250000", "350000", "650000", "200000", "850000" };

    static internal class _Config
    {
        //// how much places are contain thigs
        internal static int _ProductIndex = 0;//for the get product function
        //internal static int _OrderIndex = 0;
        //internal static int _OrderItemIndex = 0;

        //
        private static int _OrderID = 100000;
        public static int get_OrderID
        {
            get
            { return _OrderID++; }
        }

        private static int _OrderItemID = 100000;
        public static int get_OrderItemID
        {
            get
            { return _OrderItemID++; }
        }
        private static int _ProductID = 100000;
        public static int get_ProductID
        {
            get
            {
                Random randi = new Random();
                _ProductID = randi.Next(100000, 1000000);
                return _ProductID;
            }
        }
    }


    private static void _InitializeProduct()//initializ product
    {
        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            product.idOfProduct = _Config.get_ProductID;// i * 100000 + i * 20 + i * 3;//create a uniqe ID for each priduct
            product.Name = ProductName[rand.Next(0, 10)];
            var v = Enum.GetValues(typeof(Category));
            product.ProductCategory = (Category)v.GetValue(rand.Next(0, 6));
            product.InStock = rand.Next(0, 5);
            double.TryParse(Price[rand.Next(0, 7)], out double price);
            product.Price =price;
            listProduct.Add(product);
        }
    }
    private static void _InitializeOrder()//initializ order
    {
        Random rand = new Random();
        for (int i = 0; i < 20; i++)
        {
            Order _order = new Order();
            _order.idOfOrder = _Config.get_OrderID;
            _order.CustomerName = CustomerName[rand.Next(0, 7)];
            _order.DateOfShipping = null;
            TimeSpan spaceTime = TimeSpan.FromDays(1);
            _order.DateOfOrder = DateTime.Now;
            int _rand = rand.Next(0, 10);
            if (_rand <=8)
            {
                _order.DateOfShipping = (DateTime)(_order.DateOfOrder + new TimeSpan(rand.Next(0, 2), rand.Next(0, 24), rand.Next(0, 59), rand.Next(0, 59)));
                if (_rand <=4)
                {
                    _order.DateOfDelivery = _order.DateOfShipping + new TimeSpan(rand.Next(0, 7), rand.Next(0, 24), rand.Next(0, 59), rand.Next(0, 59));
                }
                else
                {
                    _order.DateOfDelivery = null;
                }
            }
            else
            {
                _order.DateOfShipping = null;
                _order.DateOfDelivery = null;
            }
            _order.CustamerAddress = "jrusalem";
            _order.CustomerMail = "maill@gmail.com";
            listOrder.Add(_order); 
        }
    }

    private static void _InitializeOrdetItem()//initializ orderItem
    {
        Random rand = new Random();
        for (int i = 0; i < 40; i++)
        {
            OrderItem _orderItem = new OrderItem();
            _orderItem.ID = _Config.get_OrderItemID;
            _orderItem.idOfOrder = listOrder[i % 19].Value.idOfOrder;//i%19 will chosse the order , in this way all the place be uesed
            int index = rand.Next(0, 10);//choss a product 
            _orderItem.idOfItem = listProduct[index].Value.idOfProduct;
            _orderItem.amount = rand.Next(1, 5);
            _orderItem.Price = listProduct[index].Value.Price;
            listOrderItem.Add(_orderItem);
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