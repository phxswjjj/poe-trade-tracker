using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader
{
    public interface IGridViewDisplay
    {
        string CurrentItemName { get; }
        string ItemName { get; }
        string Url { get; }
        int ItemCount { get; }
        string MinPrice { get; }
        string MaxPrice { get; }
        string PriceRange { get; }
        string QueryPrice { get; }
        DateTime Timestamp { get; }
    }
}
