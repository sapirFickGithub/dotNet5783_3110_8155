﻿using Dal;
using BlApi;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BlImplementation;
using BO;
using Enum = BO.Enum;
using System.Collections.Generic;
using System.Collections.Specialized;
using DO;

namespace BlTest;

public class Program

{
    static BlApi.IBl? ibl = BlApi.Factory.Get();

    public static void productOption(IBl? ibl)//fonction on product
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
                        Console.WriteLine("insert name, category, price and instock.");

                        string? name = Console.ReadLine();
                        BO.Enum.Category productCategory = (BO.Enum.Category)Enum.Category.Parse(typeof(BO.Enum.Category), Console.ReadLine());
                        double.TryParse(Console.ReadLine(), out double price);
                        int.TryParse(Console.ReadLine(), out int inStock);
                        int id = ibl.Product.addProduct(name, productCategory, price, inStock);

                        Console.WriteLine("id of the product is" + id);
                        break;
                    }
                case 'u'://updete
                    {
                        Console.WriteLine("insert id, name, category, price and instock.");
                        int.TryParse(Console.ReadLine(), out int id);
                        string? name = Console.ReadLine();
                        BO.Enum.Category productCategory = (BO.Enum.Category)Enum.Category.Parse(typeof(BO.Enum.Category), Console.ReadLine());
                        double.TryParse(Console.ReadLine(), out double price);
                        int.TryParse(Console.ReadLine(), out int inStock);
                        BO.Product product = new BO.Product()
                        {
                            idOfProduct = id,
                            Name = name,
                            ProductCategory = productCategory,
                            Price = price,
                            InStock = inStock
                        };

                        ibl.Product.Update(product);
                        break;
                    }
                case 'd'://delete
                    {
                        Console.WriteLine("insert id of product you want to delete");
                        int.TryParse(Console.ReadLine(), out int id);
                        ibl.Product.delete(id);
                        IEnumerable<BO.ProductForList> listProduct = ibl.Product.getListOfProduct();
                        foreach (var product in listProduct)
                        {
                            Console.WriteLine(product);
                        }
                        break;
                    }
                case 'l'://cart
                    {

                        //BO.ProductForList JlistProduct = (ProductForList)ibl.Product.getListOfProduct();
                        var listProduct = ibl.Product.getListOfProduct();
                        foreach (var product in listProduct)
                        {
                            Console.WriteLine(product);
                        }
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
    public static void orderOption(IBl? ibl)//fonction on order
    {
        BO.Order order = new BO.Order();
        char option;
        Console.WriteLine("hello,\n" +
            "enter g to get order,\n" +
            "enter d to updat delivery,\n" +
            "enter s to updat shipping,\n" +
            "enter t to get the tracking,\n" +
            "enter a to Admin updat order,\n" +
            "enter l to get all the order list\n" +
            "enter e to exit.");
        char.TryParse(Console.ReadLine(), out option);
        while (option != 'e')
        {
            switch (option)
            {

                case 'a'://Admin updat order
                    {
                        Console.WriteLine("insert id of order you wand to edit.");
                        int.TryParse(Console.ReadLine(), out int idOrder);
                        Console.WriteLine("insert id of product you want to chang.");
                        int.TryParse(Console.ReadLine(), out int idProduct);
                        Console.WriteLine("insert the new amount.");
                        int.TryParse(Console.ReadLine(), out int amount);
                        //if (ibl.Order.updateAdmin(idOrder, idProduct, amount))

                            Console.WriteLine(ibl.Order.GetOrder(idOrder));
                        break;
                    }
                case 'g'://get order
                    {
                        Console.WriteLine("insert id of order");
                        int.TryParse(Console.ReadLine(), out int id);
                        // order = ibl.Order.GetOrder(id);
                        Console.WriteLine(ibl.Order.GetOrder(id));
                        break;
                    }
                case 'd'://updat delivery
                    {
                        Console.WriteLine("insert id of order");
                        int.TryParse(Console.ReadLine(), out int id);
                        order = ibl.Order.updateDliveryOrder(id);
                        break;
                    }
                case 's'://updat shipping
                    {
                        Console.WriteLine("insert id of order");
                        int.TryParse(Console.ReadLine(), out int id);
                        order = ibl.Order.UpdateSupplyDelivery(id);
                        break;
                    }
                case 't'://tracking
                    {
                        Console.WriteLine("insert id of order");
                        int.TryParse(Console.ReadLine(), out int id);
                        BO.OrderTracking tracking = ibl.Order.orderTracking(id);
                        Console.WriteLine(tracking);
                        break;
                    }
                case 'l'://tracking
                    {
                        var listOrder = ibl.Order.GetAllOrderForList();
                        foreach (var product in listOrder)
                        {
                            Console.WriteLine(product);
                        }
                        break;
                    }
                case 'e'://exit
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

    public static void cartOption(IBl? ibl)//fonction on cart
    {
        string? cN, cM, cA;
        Console.WriteLine("enter name, mail, address of customer");
        cN = Console.ReadLine();
        cM = Console.ReadLine();
        cA = Console.ReadLine();
        BO.Cart cart = new BO.Cart() { CustomerName = cN, CustomerMail = cM, CustomerAddress = cA, itemList = new List<BO.OrderItem>() };
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
                        int.TryParse(Console.ReadLine(), out int id);
                        ibl.Cart.add(cart, id);
                        break;
                    }
                case 'u'://updete amount
                    {
                        Console.WriteLine("insert id of product");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine("insert the new amount");
                        int.TryParse(Console.ReadLine(), out int amount);
                        ibl.Cart.updete(cart, id, amount);
                        break;
                    }
                case 'v'://approvment
                    {
                        if (ibl.Cart.approvment(cart)>0)
                            Console.WriteLine("V");
                        else
                            Console.WriteLine("X");
                        break;
                    }

            }
            Console.WriteLine("hello\n" +
              "enter a to add cart\n" +
              "enter u to updete amount\n" +
              "enter v to approvment cart\n" +
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
                    case 'd':
                        {
                            Console.Write(ibl.Order.precedenceOrder());
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
