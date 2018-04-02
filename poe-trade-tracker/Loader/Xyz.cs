using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader
{
    public class Xyz
    {
        private Xyz(string url)
        {
            this.Url = url;
            this.IsFoundItems = false;
            this.Timestamp = DateTime.Now;
            this.Items = new List<Data.ItemInfo>();
        }
        public static Xyz Create(string url)
        {
            var xyz = new Xyz(url);

            var list = new List<string>();
            var req = WebRequest.CreateHttp(url);
            using (var resp = req.GetResponse())
            {
                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var isStartFetch = false;
                    while (!sr.EndOfStream)
                    {
                        //start: 
                        //  </script>沒有資料
                        //  </script><h4>
                        //end:
                        //  <!-- PC-市集-物品市集-RWD -->
                        var s = sr.ReadLine();
                        if (!isStartFetch)
                        {
                            if (s.StartsWith("</script><h4>"))
                            {
                                isStartFetch = true;
                                xyz.IsFoundItems = true;
                            }
                            else if (s.StartsWith("</script>沒有資料"))
                                isStartFetch = true;
                        }
                        if (isStartFetch && s.Equals("<!-- PC-市集-物品市集-RWD -->"))
                            break;
                        if (isStartFetch)
                            list.Add(s);
                    }
                }
            }
            xyz.Raws = list;
            if(xyz.IsFoundItems)
                xyz.ParseItems();

            return xyz;
        }

        private void ParseItems()
        {
            var html = string.Join(Environment.NewLine, this.Raws);
            var parser = new HtmlParser();
            var dom = parser.Parse(html);
            var itemTrs = dom.QuerySelectorAll("table#xyz_items > tbody > tr");
            foreach(var itemTr in itemTrs)
            {
                var nameSpan = itemTr.QuerySelector("span[class=\"item_unique\"]");
                var name = nameSpan.TextContent;

                var priceSpan = itemTr.QuerySelector("span[data-name=\"price\"]");

                var price = int.Parse(priceSpan.GetAttribute("data-value"));

                var priceUnitImg = priceSpan.QuerySelector("img");
                var priceUnit = priceUnitImg.GetAttribute("title");

                var item = new Data.ItemInfo(name, price, priceUnit);
                this.Items.Add(item);
            }
        }

        public string Url { get; private set; }

        public bool IsFoundItems { get; private set; }

        public DateTime Timestamp { get; private set; }

        public List<string> Raws { get; private set; }

        public List<Data.ItemInfo> Items { get; private set; }
    }
}
