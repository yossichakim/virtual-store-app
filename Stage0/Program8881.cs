using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome8881();
            Wellcome3341();

            Console.ReadKey();
        }

        static partial void Wellcome3341();
        private static void Wellcome8881()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}