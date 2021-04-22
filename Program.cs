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

            string rssNews = "";
            WebRequest request = WebRequest.Create("https://lenta.ru/rss/");

            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    rssNews = reader.ReadToEnd();
                }
            }
            Regex regex = new Regex($"<title>.*?p>(.+?)</title>", RegexOptions.Compiled | RegexOptions.Singleline);
            MatchCollection match = Regex.Matches(rssNews, $"<!\\[CDATA\\[(.*?)]]>", RegexOptions.Compiled | RegexOptions.Singleline) ;

            for (int i = 0; i < match.Count; i++)
            {
                Console.WriteLine(match[i].Groups[1].Value +"\n");
            }
            
        }

         public void  getNews(string news)
        {

        }
    }
}
