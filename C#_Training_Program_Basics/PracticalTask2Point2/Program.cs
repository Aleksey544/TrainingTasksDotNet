//Console Serializator, developed by Alexey Kuzub
using System;

namespace PracticalTask2Point2
{
    class Program
    {
        static int Main(string[] args)
        {
            CommandLineHandler.Handler(args);

            Console.WriteLine("\nPress any key!");
            Console.ReadLine();
            return 0;
        }
    }
}