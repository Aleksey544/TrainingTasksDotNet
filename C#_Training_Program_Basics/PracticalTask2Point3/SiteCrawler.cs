using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace PracticalTask2Point3
{
    //Searching for links and e-mail addresses on the site
    class SiteCrawler
    {
        public string URL { get; private set; }
        public string webText { get; private set; }
        public ISet<string> newLinks { get; private set; } = new HashSet<string>();
        public ISet<string> newEmails { get; private set; } = new HashSet<string>();
        public int linksCounter { get; private set; }
        public int emailsCounter { get; private set; }

        //Toast notification in console application
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr h, string message, string messageBox, int type);

        private delegate void AlertHundler(string message, string messageBox);
        private event AlertHundler Notify;

        private void AlertMessage(string message, string messageBox) => MessageBox((IntPtr)0, message, messageBox, 0);

        public SiteCrawler(string URL) => SetNewURL(URL);

        public void SetNewURL(string URL)
        {
            this.URL = URL;
            newLinks.Clear();
            newEmails.Clear();
            linksCounter = 0;
            emailsCounter = 0;
            GetWebText();
        }

        private void GetWebText()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                WebResponse response = request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    webText = reader.ReadToEnd();

            }
            catch (Exception)
            {
                webText = null;
            }
        }

        private void GetNewLinks()
        {
            Regex linksCrawler = new Regex(@"https?:\/\/\S+");

            foreach (Match linkMatch in linksCrawler.Matches(webText))
            {
                if (!newLinks.Contains(linkMatch.ToString()))
                    newLinks.Add(linkMatch.ToString());
            }
        }

        private void GetNewEmails()
        {
            Regex emailsCrawler = new Regex(@"\w*\.?\w+@\w+\.\w+\.?\w*");

            foreach (Match emailMatch in emailsCrawler.Matches(webText))
            {
                if (!newEmails.Contains(emailMatch.ToString()))
                    newEmails.Add(emailMatch.ToString());
            }
        }

        public void ShowLinksAndEmails()
        {
            Notify = AlertMessage;

            if (webText != null)
            {
                Parallel.Invoke(
                    () =>
                    {
                        GetNewLinks();

                        Console.ForegroundColor = ConsoleColor.Cyan;

                        foreach (string link in newLinks)
                        {
                            Console.WriteLine(link);
                            Console.WriteLine();
                            linksCounter++;
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Notify($"{linksCounter} LINKS WERE FOUND AFTER CRAWLING THE WEBSITE!", "Thread 1 finished its work!");
                    },

                    () =>
                    {
                        GetNewEmails();

                        Console.ForegroundColor = ConsoleColor.Yellow;

                        foreach (string email in newEmails)
                        {
                            Console.WriteLine(email);
                            Console.WriteLine();
                            emailsCounter++;
                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Notify($"{emailsCounter} E-MAILS WERE FOUND AFTER CRAWLING THE WEBSITE!", "Thread 2 finished its work!");
                    });
            }
            else
                Notify("YOUR URL IS UNCORRECT OR NO INTERNET CONNECTION!", "Error!");

            Console.ResetColor();
        }
    }
}
