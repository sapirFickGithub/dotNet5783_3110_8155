
using Dal;
using DO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dal;

public class Program
{
    static DalApi.IDal? dal = DalApi.Factory.Get();

/// <summary>
/// test all the fonctions on product
/// </summary>
/// <param name="dal"></param>
    public static void productOption(DalApi.IDal? dal)
    {
        char option;
       
        Console.WriteLine("Product:\n" +
                "a -add object\n" +
                "b -return the object details by getting ID\n" +
                "c -print all the object's list\n" +
                "d -delete object from the list\n" +
                "e -update the object's details\n" +
                "f -search object by getting ID\n" +
                "h -end");
        char.TryParse(Console.ReadLine(), out option);
        while (option != 'h')
        {
        Product temp = new Product();
        int id;
            switch (option)
            {
                case 'a':
                    {
                        Console.WriteLine("please enter: name, category, price, and how much of this product you have");
                        string? name;
                        name = Console.ReadLine();
                        temp.Name = name;
                        temp.ProductCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                        temp.Price = double.Parse(Console.ReadLine());
                        int.TryParse(Console.ReadLine(), out int inStock);
                        temp.InStock = inStock;
                        id = dal.Product.Add(temp);
                        break;
                    }
                case 'b':
                    {
                        Console.WriteLine("please enter ID of product");
                        int.TryParse(Console.ReadLine(), out id);
                        int i = dal.Product.search(id);
                        dal.Product.print();
                        break;
                    }
                case 'c':
                    {
                        dal.Product.print();
                        break;
                    }


                case 'd':
                    {
                        Console.WriteLine("please enter ID of product you want delete\n");
                        int.TryParse(Console.ReadLine(), out id);
                        dal.Product.delete(id);
                        dal.Product.print();
                        break;
                    }
                case 'e':
                    {
                        Console.WriteLine("please enter: ID, name, category, price, and how much of this product you have");
                        int.TryParse(Console.ReadLine(), out int idProduct);
                        temp.idOfProduct = idProduct;
                        temp.Name = Console.ReadLine();
                        temp.ProductCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                        temp.Price = double.Parse(Console.ReadLine());
                        int.TryParse(Console.ReadLine(), out int inStock);
                        temp.InStock = inStock;
                        dal.Product.update(temp);
                        break;
                    }
                case 'f':
                    {
                        Console.WriteLine("please enter ID\n");
                        id = Console.Read();
                        Console.WriteLine("the index is" + dal.Product.search(id));

                        break;
                    }
            }
            Console.WriteLine("\n\n\nProduct:\n" +
        "a -add object\n" +
        "b -return the object details by getting ID\n" +
        "c -print all the object's list\n" +
        "d -delete object from the list\n" +
        "e -update the object's details\n" +
        "f -search object by getting ID\n" +
        "h -end");
            char.TryParse(Console.ReadLine(), out option);


            }
    }
    /// <summary>
    ///  test all the fonctions on order
    /// </summary>
    /// <param name="dal"></param>
    public static void orderOption(DalApi.IDal? dal)
    {
        char option;
        Console.WriteLine("Order \n" +
                  "a -add object\n" +
                  "b -return the object details by getting ID\n" +
                  "c -print all the object's list\n" +
                  "d -delete object from the list\n" +
                  "e -update the object's details\n" +
                  "f  -search object by getting ID\n" +
                  "h -end\n");
        
        char.TryParse(Console.ReadLine(), out option);
        Order temp = new Order();
        int id;
        while (option != 'h')
        {

            switch (option)
            {
                case 'a':
                    {
                        Console.WriteLine(" please enter: customer name, mail,and address");
                        temp.CustomerName = Console.ReadLine();
                        temp.CustomerMail = Console.ReadLine();
                        temp.CustamerAddress = Console.ReadLine();
                        id = dal.Order.Add(temp);
                        break;
                    }
                case 'b':
                    {
                        Console.WriteLine("please enter ID of Order");
                        int.TryParse(Console.ReadLine(), out id);
                        int i = dal.Order.search(id);
                        dal.Order.print();
                        break;
                    }
                case 'c':
                    {
                            dal.Order.print();
                        break;
                    }
                case 'd':
                    {
                        Console.WriteLine("please enter ID of Order you want delete\n");
                        int.TryParse(Console.ReadLine(), out id);
                        dal.Order.delete(id);
                        dal.Order.print();
                        break;
                    }
                case 'e':
                    {
                        Console.WriteLine("please enter: ID, customer name, mail,and address");
                        int.TryParse(Console.ReadLine(), out int idOrder);
                        temp.idOfOrder = idOrder;
                        temp.CustomerName = Console.ReadLine();
                        temp.CustomerMail = Console.ReadLine();
                        temp.CustamerAddress = Console.ReadLine();
                        dal.Order.update(temp);
                        break;
                    }
                case 'f':
                    {
                        Console.WriteLine("please enter ID\n");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("the index is" + dal.Order.search(id));

                        break;
                    }
            }
            Console.WriteLine("Order \n" +
                 "a -add object\n" +
                 "b -return the object details by getting ID\n" +
                 "c -print all the object's list\n" +
                 "d -delete object from the list\n" +
                 "e -update the object's details\n" +
                 "f  -search object by getting ID\n" +
                 "h -end\n");

            char.TryParse(Console.ReadLine(), out option);
        }
    }
    /// <summary>
    ///  test all the fonctions on order item
    /// </summary>
    /// <param name="dal"></param>
    public static void orderItem(DalApi.IDal? dal)
    {
        char option;
        Console.WriteLine("Order Item\n" +
                    "a -add object\n" +
                    "b -return the object details by getting ID\n" +
                    "c -print all the object's list\n" +
                    "d -delete object from the list\n" +
                    "e -update the object's details\n" +
                    "f -search object by getting ID\n" +
                    "h -end\n");
        char.TryParse(Console.ReadLine(), out option);
        OrderItem temp = new OrderItem();
        int id;
        while (option != 'h')
        {
            switch(option)
            {
                case 'a':
                    {
                        Console.WriteLine("please enter (ProductID,OrderID,Price,Amount)");
                        int ProductID;
                        int OrderID;
                        double price;
                        int Amount;
                        int.TryParse(Console.ReadLine(), out ProductID);
                        int.TryParse(Console.ReadLine(), out OrderID);
                        double.TryParse(Console.ReadLine(), out price);
                        int.TryParse(Console.ReadLine(), out Amount);
                        id = dal.OrderItem.Add(temp);
                        break;
                    }
                case 'b':
                    {
                        Console.WriteLine("please enter ID of Order");
                        int.TryParse(Console.ReadLine(), out id);
                        int i = dal.OrderItem.search(id);
                        dal.OrderItem.print();
                        break;
                    }
                case 'c':
                    {
                        for (int i = 0; i < dal.OrderItem.length(); i++)
                        {
                            Console.Write((i + 1) + "-");
                            dal.OrderItem.print();

                        }
                        break;
                    }
                case 'd':
                    {
                        Console.WriteLine("please enter ID of Order you want delete\n");
                        int.TryParse(Console.ReadLine(), out id);
                        dal.OrderItem.delete(id);
                        dal.OrderItem.print();
                        break;
                    }
                case 'e':
                    {
                        Console.WriteLine("please enter: customer name, mail,and address");
                        int ProductID;
                        int OrderID;
                        double price;
                        int Amount;
                        int.TryParse(Console.ReadLine(), out ProductID);
                        int.TryParse(Console.ReadLine(), out OrderID);
                        double.TryParse(Console.ReadLine(), out price);
                        int.TryParse(Console.ReadLine(), out Amount);
                        dal.OrderItem.update(temp);
                        break;
                    }
                case 'f':
                    {
                        Console.WriteLine("please enter ID\n");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("the index is" + dal.OrderItem.search(id));

                        break;
                    }
            }
            Console.WriteLine("Order Item\n" +
                    "a -add object\n" +
                    "b -return the object details by getting ID\n" +
                    "c -print all the object's list\n" +
                    "d -delete object from the list\n" +
                    "e -update the object's details\n" +
                    "f -search object by getting ID\n" +
                    "h -end\n");
            char.TryParse(Console.ReadLine(), out option);

        }
    }

    public static void Main(string[] args)
    {
        DalApi.IDal? dal = DalApi.Factory.Get();

        char num = '5';
        Console.WriteLine("press 0 to exit\nfor Product press 1\nfor Ordr press 2\nfor OrderItem press 3");
        while (num != '0')
        {
            try
            {

                char.TryParse(Console.ReadLine(), out num);

                switch (num)
                {

                    case '0'://exit
                        {
                            return;
                        }
                    case '1'://Product
                        {
                            productOption(dal);
                            break;
                        }
                    case '2'://Order
                        {
                            orderOption(dal);
                            break;
                        }
                    case '3'://orderitem
                        {

                            orderItem(dal);
                            break;
                        }
                }
                Console.WriteLine("PLEASE ENTER YOUR NEXT CHOICE");

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

    }
}