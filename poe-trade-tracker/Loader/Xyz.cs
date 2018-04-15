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
    public class Xyz : ILoader, IGridViewDisplay
    {
        private IEnumerable<string> _blacklists = null;
        private Xyz(Xyz cloneFrom)
        {
            this.Url = cloneFrom.Url;
            this.IsValid = cloneFrom.IsValid;
            this.IsFoundItems = cloneFrom.IsFoundItems;
            this.ItemName = cloneFrom.ItemName;
            this.Timestamp = cloneFrom.Timestamp;
            this.Raws = cloneFrom.Raws;
            this.Items = cloneFrom.Items;
            this.Query = cloneFrom.Query;
        }
        private Xyz(string url, string itemName)
            : this(url)
        {
            if (!string.IsNullOrEmpty(itemName))
                this.ItemName = itemName;
        }
        private Xyz(string url)
        {
            this.Url = url;
            this.Init();
        }
        public static Xyz Create(string url, string itemName)
        {
            var xyz = new Xyz(url, itemName);
            try
            {
                xyz.GetInfo();
            }
            catch (System.UriFormatException)
            {
                return null;
            }

            return xyz;
        }
        public static Xyz Create(string url)
        {
            return Create(url, "");
        }

        private void Init(bool isReloadUrl)
        {
            this.IsValid = false;
            this.IsFoundItems = false;
            this.Raws = new List<string>();
            this.Items = new List<Data.ItemInfo>();
            this.Timestamp = DateTime.Now;
            if(isReloadUrl)
            {
                this.Url = null;
                this.ItemName = null;
                this.Query = null;
            }
        }
        private void Init()
        {
            this.Init(false);
        }

        private void GetInfo()
        {
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
            {
                this.ParseHtmlItems();
                if (this.Items.Count > 0 && string.IsNullOrEmpty(this.ItemName))
                    this.ItemName = this.Items.First().Name;
                ApplyBlacklist();
            }

            if (this.Query == null)
                this.ParseUrl();

            this.Timestamp = DateTime.Now;
        }

        private Xyz Clone()
        {
            var xyz = new Xyz(this);
            return xyz;
        }

        public void Reload(string newUrl)
        {
            if (!string.IsNullOrEmpty(newUrl))
            {
                this.Init(true);
                this.Url = newUrl.Trim();
            }

            this.GetInfo();
        }
        public void Reload()
        {
            Reload(null);
        }

        public void SetBlacklist(IEnumerable<string> blacklists)
        {
            this._blacklists = blacklists;
            ApplyBlacklist();
        }

        private void ApplyBlacklist()
        {
            if (this._blacklists == null)
                return;
            foreach (var blacklist in this._blacklists)
                this.Items.RemoveAll(item => item.Account.Equals(blacklist));
        }

        private void ParseHtmlItems()
        {
            var html = string.Join(Environment.NewLine, this.Raws);
            var parser = new HtmlParser();
            var dom = parser.Parse(html);
            var itemTrs = dom.QuerySelectorAll("table#xyz_items > tbody > tr");
            foreach (var itemTr in itemTrs)
            {
                var nameSpan = itemTr.QuerySelector("span[class=\"item_unique\"]");
                if (nameSpan == null)
                {
                    //normal item
                    nameSpan = itemTr.QuerySelector("td:nth-child(2) > span:nth-child(2)[\"class\"]");
                }
                var name = nameSpan.TextContent.Trim();

                var priceSpan = itemTr.QuerySelector("span[data-name=\"price\"]");
                var price = int.Parse(priceSpan.GetAttribute("data-value"));

                var priceUnitImg = priceSpan.QuerySelector("img");
                var priceUnit = priceUnitImg.GetAttribute("title");

                var accountHref = itemTr.QuerySelector("a.xyz_user2");
                var href = accountHref.GetAttribute("href");
                var hrefUri = new Uri(href);
                var account = hrefUri.Segments.Last();

                var item = new Data.ItemInfo(name, price, priceUnit, account);
                this.Items.Add(item);
            }
        }
        private void ParseUrl()
        {
            var q = new Data.QueryCondition();
            this.Query = q;
            var uri = new Uri(this.Url);
            var queryStrings = System.Web.HttpUtility.ParseQueryString(uri.Query);

            var name = queryStrings.Get("name");
            q.Name = name;
            if (string.IsNullOrEmpty(this.ItemName) && !string.IsNullOrEmpty(name))
                this.ItemName = Uri.UnescapeDataString(name);

            int iCurrency;
            if (int.TryParse(queryStrings.Get("boi"), out iCurrency) && iCurrency > 0)
                q.Currency = (Data.CurrencyType)iCurrency;

            int iPrice;
            if (int.TryParse(queryStrings.Get("boc_min"), out iPrice) && iPrice > 0)
                q.MinPrice = iPrice;
            if (int.TryParse(queryStrings.Get("boc_max"), out iPrice) && iPrice > 0)
                q.MaxPrice = iPrice;
        }

        public string Url { get; private set; }

        public bool IsValid { get; private set; }

        public bool IsFoundItems { get; private set; }

        public string ItemName { get; private set; }

        public DateTime Timestamp { get; private set; }

        public List<string> Raws { get; private set; }

        public List<Data.ItemInfo> Items { get; private set; }

        public Data.QueryCondition Query { get; private set; }

        public string CurrentItemName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Query.Name))
                    return this.Query.Name;
                if (!string.IsNullOrEmpty(this.ItemName))
                    return this.ItemName;
                if (this.Items.Count > 0)
                    return this.Items.First().Name;
                return string.Empty;
            }
        }
        string IGridViewDisplay.ItemName
        {
            get
            {
                var name = this.CurrentItemName;
                if (string.IsNullOrEmpty(name))
                    return "unknown";
                else
                    return name;
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

        string IGridViewDisplay.MaxPrice
        {
            get
            {
                var item = this.Items.OrderByDescending(x => x.Price).FirstOrDefault();
                if (item != null)
                    return $"{item.Price} {item.PriceUnit}";
                return "";
            }
        }

        DateTime IGridViewDisplay.Timestamp => this.Timestamp;

        string IGridViewDisplay.CurrentItemName => this.CurrentItemName;

        string IGridViewDisplay.PriceRange
        {
            get
            {
                var sortedItems = this.Items.OrderBy(x => x.Price);
                var firstItem = sortedItems.FirstOrDefault();
                if (firstItem == null)
                    return "";
                var lastItem = sortedItems.Last();
                if (firstItem.PriceUnit.Equals(firstItem.PriceUnit))
                    return $"{firstItem.Price} ~ {lastItem.Price} {firstItem.PriceUnit}";
                else
                    return $"{firstItem.Price} {firstItem.PriceUnit} ~ {lastItem.Price} {lastItem.PriceUnit}";
            }
        }

        string IGridViewDisplay.QueryPrice
        {
            get
            {
                var fromPriceString = "?";
                var toPriceString = "?";
                if (this.Query.MinPrice > 0)
                    fromPriceString = this.Query.MinPrice.ToString();
                if (this.Query.MaxPrice > 0)
                    toPriceString = this.Query.MaxPrice.ToString();

                var priceUnit = "unknown";
                var miCurrency = typeof(Data.CurrencyType).GetMember(this.Query.Currency.ToString()).First();
                var enumAttr = Attribute.GetCustomAttribute(miCurrency, typeof(Data.EnumAttribute)) as Data.EnumAttribute;
                if (enumAttr != null)
                    priceUnit = enumAttr.Description;
                return $"{fromPriceString} ~ {toPriceString} {priceUnit}";
            }
        }
    }
}
