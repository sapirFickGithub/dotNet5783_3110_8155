
using Dal;
using DO;
using System.Runtime.CompilerServices;
public class Program
{
    private DalOrder _dalOrder = new DalOrder();
    private DalOrderItem _dalOrderItem = new DalOrderItem();
    private DalProduct _dalProduct = new DalProduct();
    public static void productOption(char option)
    {
        switch(option)
        {
            case 'a':
                {
                    break;
                }

        }
    }
    public static void orderOption(char option)
    {
        switch (option)
        {

        }
    }
    public static void orderItem(char option)
    {
        switch (option)
        {

        }
    }

    public static void Main()
    {
        char option;
        int num;
        Console.WriteLine("press any key\nfor Product press 1\n for Ordr press 2\n for OrderItem press 3");
           num = Console.Read();
        switch (num)
        {
            case 0://exit
                {
                    return;
                }
            case 1://Product
                {
                    option = char.Parse(Console.ReadLine());//
                    productOption(option);
                    break;
                }
            case 2://Order
                {
                    option = char.Parse(Console.ReadLine());
                    orderOption(option);
                    break;
                }
            case 3://orderitem
                {
                    option = char.Parse(Console.ReadLine());
                    orderItem(option);
                    break;
                }
        }

    }
}