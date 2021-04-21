using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace NewsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputHtml = "";

            string htmlNews = "";
            WebRequest request = WebRequest.Create("https://ria.ru/product_astrologiya/");

            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    htmlNews = reader.ReadToEnd();
                }
            }
            Regex regex = new Regex($"list-item__title.*?p>(.+?)<", RegexOptions.Compiled | RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(htmlNews);
            Console.WriteLine(regex.IsMatch(htmlNews));
        }

         public void  getNews(string news)
        {

        }
    }
}
