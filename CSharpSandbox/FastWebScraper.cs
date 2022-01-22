using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace learning_cSharp
{
    public class FastWebScraper
    {
        public List<Prayer> GetPrayers(string url)
        {
            List<Prayer> prayers = new List<Prayer>();
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div[5]/div/div/div/section/div/div/div[3]/div/div/div/div/div[2]/div/form/div[5]/div/div/table/tbody/tr/td[2]/table/tbody/tr[position()>0]");
            foreach (var node in nodes)
            {
                var prayer = new Prayer
                {
                    PrayerName = node.SelectSingleNode("td[1]").InnerText,
                    PrayerTime = node.SelectSingleNode("td[2]").InnerText,
                };
                prayers.Add(prayer);
            }

            return prayers;
        }
    }
}
