using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Data
{
    public class QueryCondition
    {
        public string Name { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
