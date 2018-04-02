using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Data
{
    public class ItemInfo
    {
        public ItemInfo(string name, long price, string priceUnit)
        {
            this.Name = name;
            this.Price = price;
            this.PriceUnit = priceUnit;
        }
        public string Name { get; private set; }
        public long Price { get; private set; }
        public string PriceUnit { get; private set; }
    }
}
