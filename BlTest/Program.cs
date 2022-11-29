using Dal;
using BlApi;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BlImplementation;

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
                        Console.WriteLine("insert id, name, category, price and instock.";)
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

                        break;
                    }
                case 'l'://cart
                    {

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
        char option;
        Console.WriteLine("hello\n" +
            "enter g to get order\n" +
            "enter d to updat delivery\n" +
            "enter s to updat shipping\n" +
            "enter e to exit");
        char.TryParse(Console.ReadLine(), out option);
        while (option != 'e')
        {
            switch (option)
            {

                case 'g'://exit
                    {

                        break;
                    }
                case 'd'://Product
                    {

                        break;
                    }
                case 's'://Order
                    {

                        break;
                    }
                case 'e'://cart
                    {
                        return;
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

    public static void cartOption(IBl ibl)//fonction on cart
    {

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