using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PracticeConsoleApp.Infrastructure
{
    public class RemoteApiParser
    {
        public static int GetRezult(string url, string htmlTag, string tagName)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip |
                                        DecompressionMethods.Deflate
            };

            int randomNumber = 0;

            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.BaseAddress = new Uri(url);

                using (Stream stream = httpClient.GetStreamAsync("").Result)
                {
                    HtmlDocument document = new HtmlDocument();
                    document.Load(stream);
                    randomNumber = Convert.ToInt32(document.DocumentNode.SelectSingleNode($".//{htmlTag}[@class='{tagName}']").InnerText);
                }
            }

            return randomNumber;
        }
    }
}
