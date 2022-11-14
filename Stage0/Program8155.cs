// See https://aka.ms/new-console-template for more information
using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8155();
            Welcome3110();


        }
        static partial void Welcome3110();

        private static void Welcome8155()
        {
            Console.WriteLine("enter name");
            string name = Console.ReadLine();
            Console.Write("tnx for ");
            Console.ReadKey();
        }
    }
}
