using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace NewsParser
{
    class newsParser
    {
        static void Main(string[] args)
        {
            //Создаем новый поток, который будет отправлять сообщения в телеграмм
            Thread myThread = new Thread(new ThreadStart(sendNews));
            myThread.Start();
            TimeSpan newsInterval = new TimeSpan(1, 0, 0);
            
            while (true)
            {
                Console.WriteLine(getNews());
                Thread.Sleep(newsInterval);
            }
            
        }

         public static string  getNews()
        {
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
            string pattern = "<link>(.*?)</link>"; 
            MatchCollection matches = Regex.Matches(rssNews, pattern, RegexOptions.Compiled | RegexOptions.Singleline); // парсим ссылку на новость
            MatchCollection match = Regex.Matches(rssNews, $"<!\\[CDATA\\[(.*?)]]>", RegexOptions.Compiled | RegexOptions.Singleline); //парсим новость

            
            return ($"{match[0].Groups[1].Value}\n{matches[2].Groups[1].Value}").ToString();
        }

        private static void sendNews()
        {
            // В данном методе должен вызываться метод sendMessage
        }
    }
}
