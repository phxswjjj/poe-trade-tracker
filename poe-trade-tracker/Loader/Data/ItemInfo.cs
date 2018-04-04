using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Data
{
    public class ItemInfo
    {
        public ItemInfo(string name, long price, string priceUnit, string account)
        {
            this.Name = name;
            this.Price = price;
            this.PriceUnit = priceUnit;
            this.Account = Uri.UnescapeDataString(account);
        }
        public string Name { get; private set; }
        public long Price { get; private set; }
        public string PriceUnit { get; private set; }
        public string Account { get; private set; }
    }
}
