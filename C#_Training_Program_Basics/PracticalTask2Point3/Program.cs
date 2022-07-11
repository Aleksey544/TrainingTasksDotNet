//Console Site Crawler, developed by Alexey Kuzub
using System;

namespace PracticalTask2Point3
{
    class Program
    {
        public static void Main(string[] args)
        {
            //testing site: https://www.theguardian.com/technology/1999/jan/17/internet.theobserver7

            Console.Write("Input website's main html page URL (https://example.com/): ");
            string URL = Console.ReadLine();
            Console.WriteLine();

            SiteCrawler crawler = new SiteCrawler(URL);
            crawler.ShowLinksAndEmails();

            Console.WriteLine("\nApplication finished working succesfully! Press any key!");
            Console.ReadLine();
        }
    }
}
