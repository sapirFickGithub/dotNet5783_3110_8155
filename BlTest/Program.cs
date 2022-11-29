using Dal;
using BlApi;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BlImplementation;
using BO;

namespace BlTest;

public class Program

{
    static IBl ibl = new BL();

    public static void productOption(IBl ibl)//fonction on product
    {
        char option;
        Console.WriteLine("hello\n" +
            "enter a to add product\n" +
            "enter u to updete product\n" +
            "enter d to delete product\n" +
            "enter l to get all the productin list\n" +
            "enter e to exit");
        char.TryParse(Console.ReadLine(), out option);
        while (option != 'e')
        {
            switch (option)
            {

                case 'e'://exit
                    {
                        return;
                    }
                case 'a'://Product
                    {
                        Console.WriteLine("insert id, name, category, price and instock.");
                        int id = Console.Read();
                        string? name = Console.ReadLine();
                        BO.Category productCategory = (BO.Category)Enum.Parse(typeof(BO.Category), Console.ReadLine());
                        double price = double.Parse(Console.ReadLine());
                        int inStock = Console.Read();

                        ibl.Product.add(id, name, productCategory, price, inStock);
                        break;
                    }
                case 'u'://updete
                    {
                        Console.WriteLine("insert id, name, category, price and instock.");
                        int id = Console.Read();
                        string? name = Console.ReadLine();
                        BO.Category productCategory = (BO.Category)Enum.Parse(typeof(BO.Category), Console.ReadLine());
                        double price = double.Parse(Console.ReadLine());
                        int inStock = Console.Read();
                        BO.Product product = new BO.Product()
                        {
                            ID = id,
                            Name = name,
                            ProductCategory = productCategory,
                            Price = price,
                            InStock = inStock
                        };

                        ibl.Product.updat(product);
                        break;
                    }
                case 'd'://delete
                    {
                        Console.WriteLine("insert id of product you want to delete");
                        int id = Console.Read();
                        ibl.Product.delete(id);
                        break;
                    }
                case 'l'://cart
                    {

                        BO.ProductForList listProduct = ibl.Product.allProduct();

                      /////////////////////////////////////////////////////////
                        break;
                    }

            }
            Console.WriteLine("hello\n" +
             "enter a to add product\n" +
             "enter u to updete product\n" +
             "enter d to delete product\n" +
             "enter l to get all the productin list\n" +
             "enter e to exit");
            char.TryParse(Console.ReadLine(), out option);
        }
    }
    public static void orderOption(IBl ibl)//fonction on order
    {
        BO.Order order = new BO.Order();
        char option;
        Console.WriteLine("hello\n" +
            "enter g to get order\n" +
            "enter d to updat delivery\n" +
            "enter s to updat shipping\n" +
            "enter t to get the tracking\n" +
            "enter e to exit");
        char.TryParse(Console.ReadLine(), out option);
        while (option != 'e')
        {
            switch (option)
            {

                case 'g'://exit
                    {
                        Console.WriteLine("insert id of order");
                        int id = Console.Read();
                        order = ibl.Order.get(id);
                        break;
                    }
                case 'd'://Product
                    {
                        Console.WriteLine("insert id of order");
                        int id = Console.Read();
                        order = ibl.Order.updatDelivery(id);
                        break;
                    }
                case 's'://Order
                    {
                        Console.WriteLine("insert id of order");
                        int id = Console.Read();
                        order = ibl.Order.updatShipping(id);
                        break;
                    }
                case 't'://tracking
                    {
                        Console.WriteLine("insert id of order");
                        int id = Console.Read();
                        BO.OrderTracking tracking = ibl.Order.tracking(id);
                        Console.WriteLine(tracking);
                        break;
                    }
                case 'e'://cart
                    {
                        return;
                    }

            }
            Console.WriteLine("hello\n" +
            "enter g to get order\n" +
            "enter d to updat delivery\n" +
            "enter s to updat shipping\n" +
             "enter t to get the tracking\n" +
            "enter e to exit");
            char.TryParse(Console.ReadLine(), out option);
        }

    }

    public static void cartOption(IBl ibl)//fonction on cart
    {
        string? cN, cM, cA;
        Console.WriteLine("enter name, mail, address of customer");
        cN = Console.ReadLine();
        cM = Console.ReadLine();
        cA = Console.ReadLine();
        BO.Cart cart = new BO.Cart()
        {
            CustomerName = cN,
            CustomerMail = cM,
            CustomerAddress = cA
        };
        char option;
        Console.WriteLine("hello\n" +
            "enter a to add cart\n" +
            "enter u to updete amount\n" +
            "enter v to approvment cart\n" +
            "enter e to exit");
        char.TryParse(Console.ReadLine(), out option);

        while (option != 'e')
        {
            switch (option)
            {

                case 'e'://exit
                    {
                        return;
                    }
                case 'a'://add cart
                    {
                        Console.WriteLine("insert id of product");
                        int id = Console.Read();
                        ibl.Cart.add(cart, id);
                        break;
                    }
                case 'u'://updete amount
                    {
                        Console.WriteLine("insert id of product");
                        int id = Console.Read();
                        Console.WriteLine("insert the new amount");
                        int amount = Console.Read();
                        ibl.Cart.updetAmount(cart, id, amount);
                        break;
                    }
                case 'v'://approvment
                    {
                        if (ibl.Cart.approvment(cart))
                            Console.WriteLine("V");
                        else
                            Console.WriteLine("X");
                        break;
                    }

            }
            Console.WriteLine("hello\n" +
          "enter p to function on product\n" +
          "enter c to function on cart\n" +
          "enter o to function on order\n" +
          "enter e to exit");
            char.TryParse(Console.ReadLine(), out option);
        }
    }

    static void Main(string[] args)
    {
        char option;
        Console.WriteLine("hello\n" +
            "enter p to function on product\n" +
            "enter o to function on order\n" +
            "enter c to function on cart\n" +
            "enter e to exit");
        char.TryParse(Console.ReadLine(), out option);
        try
        {
            while (option != 'e')
            {
                switch (option)
                {

                    case 'e'://exit
                        {
                            return;
                        }
                    case 'p'://Product
                        {
                            productOption(ibl);
                            break;
                        }
                    case 'o'://Order
                        {
                            orderOption(ibl);
                            break;
                        }
                    case 'c'://cart
                        {

                            cartOption(ibl);
                            break;
                        }

                }
                Console.WriteLine("hello\n" +
              "enter p to function on product\n" +
              "enter c to function on cart\n" +
              "enter o to function on order\n" +
              "enter e to exit");
                char.TryParse(Console.ReadLine(), out option);
            }
        }
        catch (Exception expetion)
        {
            Console.WriteLine(expetion.Message);
        }
    }
}