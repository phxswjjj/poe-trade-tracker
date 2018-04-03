﻿using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader
{
    public class Xyz : IGridViewDisplay
    {
        private Xyz(Xyz cloneFrom)
        {
            this.Url = cloneFrom.Url;
            this.IsValid = cloneFrom.IsValid;
            this.IsFoundItems = cloneFrom.IsFoundItems;
            this.Timestamp = cloneFrom.Timestamp;
            this.Raws = cloneFrom.Raws;
            this.Items = cloneFrom.Items;
            this.PreviousResult = cloneFrom.PreviousResult;
        }
        private Xyz(string url)
        {
            this.Url = url;
            this.Init();
        }
        public static Xyz Create(string url)
        {
            var xyz = new Xyz(url);
            xyz.GetInfo();

            return xyz;
        }

        private void Init()
        {
            this.IsValid = false;
            this.IsFoundItems = false;
            this.Raws = new List<string>();
            this.Items = new List<Data.ItemInfo>();
            this.Timestamp = DateTime.Now;
        }

        private void GetInfo()
        {
            this.Init();

            var req = WebRequest.CreateHttp(this.Url);
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
                                this.IsFoundItems = true;
                            }
                            else if (s.StartsWith("</script>沒有資料"))
                                isStartFetch = true;
                        }
                        if (isStartFetch && s.Equals("<!-- PC-市集-物品市集-RWD -->"))
                        {
                            this.IsValid = true;
                            break;
                        }
                        if (isStartFetch)
                            this.Raws.Add(s);
                    }
                }
            }
            if (this.IsFoundItems)
                this.ParseItems();

            this.Timestamp = DateTime.Now;
        }

        private Xyz Clone()
        {
            var xyz = new Xyz(this);
            return xyz;
        }

        public void Reload()
        {
            this.PreviousResult = this.Clone();

            this.GetInfo();
        }

        private void ParseItems()
        {
            var html = string.Join(Environment.NewLine, this.Raws);
            var parser = new HtmlParser();
            var dom = parser.Parse(html);
            var itemTrs = dom.QuerySelectorAll("table#xyz_items > tbody > tr");
            foreach (var itemTr in itemTrs)
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

        public bool IsValid { get; private set; }

        public bool IsFoundItems { get; private set; }

        public DateTime Timestamp { get; private set; }

        public List<string> Raws { get; private set; }

        public List<Data.ItemInfo> Items { get; private set; }

        public Xyz PreviousResult { get; private set; }

        string IGridViewDisplay.ItemName
        {
            get
            {
                if (this.Items.Count > 0)
                    return this.Items.First().Name;
                return "unknown";
            }
        }

        string IGridViewDisplay.Url => this.Url;

        int IGridViewDisplay.ItemCount => this.Items.Count;

        string IGridViewDisplay.MinPrice
        {
            get
            {
                var item = this.Items.OrderBy(x => x.Price).FirstOrDefault();
                if (item != null)
                    return $"{item.Price} {item.PriceUnit}";
                return "";
            }
        }

        DateTime IGridViewDisplay.Timestamp => this.Timestamp;
    }
}
