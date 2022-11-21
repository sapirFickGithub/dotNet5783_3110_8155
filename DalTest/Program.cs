
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


    public static void productOption(ref DalProduct dalProduct)
    {
        char option;
        Console.WriteLine("Order Item\n" +
                "a -add object\n" +
                "b -return the object details by getting ID\n" +
                "c -print all the object's list\n" +
                "d -delete object from the list\n" +
                "e -update the object's details\n" +
                "f -search object by getting ID\n" +
                "h -end");

        char.TryParse(Console.ReadLine(), out option);
        Product temp = new Product();
        int id;

        switch (option)
        {
            case 'a':
                {
                    Console.WriteLine("please enter: name, category, price, and how much of this product you have");
                    string name;
                    name = Console.ReadLine();
                    temp.Name = name;
                    //temp.Name = Console.ReadLine();
                    temp.ProductCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                    temp.Price = double.Parse(Console.ReadLine());
                    int.TryParse(Console.ReadLine(), out int inStock);
                    temp.InStock = inStock;
                    id = dalProduct.Add(temp);
                    break;
                }
            case 'b':
                {
                    Console.WriteLine("please enter ID of product");
                    int.TryParse(Console.ReadLine(), out id);
                    int i = dalProduct.search(id);
                    dalProduct.print(i);
                    break;
                }
            case 'c':
                {

                    for (int _i = 0; _i < dalProduct.length(); _i++)
                    { // print all the order items
                        dalProduct.print(_i);
                    }
                    break;
                }


            case 'd':
                {
                    Console.WriteLine("please enter ID of product you want delete\n");
                    int.TryParse(Console.ReadLine(), out id);
                    dalProduct.delete(id);
                    dalProduct.print(id);
                    break;
                }
            case 'e':
                {
                    Console.WriteLine("please enter: name, category, price, and how much of this product you have");
                    temp.Name = Console.ReadLine();
                    temp.ProductCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                    temp.Price = double.Parse(Console.ReadLine());
                    int.TryParse(Console.ReadLine(), out int inStock);
                    temp.InStock = inStock;
                    dalProduct.update(temp);
                    break;
                }
            case 'f':
                {
                    Console.WriteLine("please enter ID\n");
                    id = Console.Read();
                    Console.WriteLine("the index is" + dalProduct.search(id));

                    break;
                }

        }
    }
    public static void orderOption(ref DalOrder dalOrder)
    {
        Console.WriteLine("Order Item\n" +
                  "a -add object\n" +
                  "b -return the object details by getting ID\n" +
                  "c -print all the object's list\n" +
                  "d -delete object from the list\n" +
                  "e -update the object's details\n" +
                  "f  -search object by getting ID\n" +
                  "h -end\n");
        char option;
        char.TryParse(Console.ReadLine(), out option);
        Order temp = new Order();
        int id;

        switch (option)
        {
            case 'a':
                {
                    Console.WriteLine(" please enter: customer name, mail,and address");
                    temp.CustomerName = Console.ReadLine();
                    temp.CustomerMail = Console.ReadLine();
                    temp.CustamerAddress = Console.ReadLine();
                    id = dalOrder.Add(temp);
                    break;
                }
            case 'b':
                {
                    Console.WriteLine("please enter ID of Order");
                    int.TryParse(Console.ReadLine(), out id);
                    int i = dalOrder.search(id);
                    dalOrder.print(i);
                    break;
                }
            case 'c':
                {
                    for (int i = 0; i < dalOrder.length(); i++)
                    {
                        Console.Write((i + 1) + "-");
                        dalOrder.print(i);

                    }
                    break;
                }
            case 'd':
                {
                    Console.WriteLine("please enter ID of Order you want delete\n");
                    int.TryParse(Console.ReadLine(), out id);
                    dalOrder.delete(id);
                    dalOrder.print(id);
                    break;
                }
            case 'e':
                {
                    Console.WriteLine("please enter: customer name, mail,and address");
                    temp.CustomerName = Console.ReadLine();
                    temp.CustomerMail = Console.ReadLine();
                    temp.CustamerAddress = Console.ReadLine();
                    dalOrder.update(temp);
                    break;
                }
            case 'f':
                {
                    Console.WriteLine("please enter ID\n");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("the index is" + dalOrder.search(id));

                    break;
                }
        }
    }
    public static void orderItem(ref DalOrderItem dalOrderItem)
    {
        Console.WriteLine("Order Item\n" +
                    "a -add object\n" +
                    "b -return the object details by getting ID\n" +
                    "c -print all the object's list\n" +
                    "d -delete object from the list\n" +
                    "e -update the object's details\n" +
                    "f -search object by getting ID\n" +
                    "h -end\n");
        char.TryParse(Console.ReadLine(), out char option);
        OrderItem temp = new OrderItem();
        int id;
        switch (option)
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
                    id = dalOrderItem.Add(temp);
                    break;
                }
            case 'b':
                {
                    Console.WriteLine("please enter ID of Order");
                    int.TryParse(Console.ReadLine(), out id);
                    int i = dalOrderItem.search(id);
                    dalOrderItem.print(i);
                    break;
                }
            case 'c':
                {
                    for (int i = 0; i < dalOrderItem.length(); i++)
                    {
                        Console.Write((i + 1) + "-");
                        dalOrderItem.print(i);

                    }
                    break;
                }
            case 'd':
                {
                    Console.WriteLine("please enter ID of Order you want delete\n");
                    int.TryParse(Console.ReadLine(), out id);
                    dalOrderItem.delete(id);
                    dalOrderItem.print(id);
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
                    dalOrderItem.update(temp);
                    break;
                }
            case 'f':
                {
                    Console.WriteLine("please enter ID\n");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("the index is" + dalOrderItem.search(id));

                    break;
                }
        }
    }

    public static void Main(string[] args)
    {

        DalOrder dalOrder = new DalOrder();
        DalOrderItem dalOrderItem = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();
        char num = '5';
        Console.WriteLine("press 0 to exit\nfor Product press 1\n for Ordr press 2\n for OrderItem press 3");
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
                            productOption(ref dalProduct);
                            break;
                        }
                    case '2'://Order
                        {
                            orderOption(ref dalOrder);
                            break;
                        }
                    case '3'://orderitem
                        {

                            orderItem(ref dalOrderItem);
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