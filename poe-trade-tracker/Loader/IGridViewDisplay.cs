﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader
{
    interface IGridViewDisplay
    {
        string ItemName { get; }
        string Url { get; }
        int ItemCount { get; }
        string MinPrice { get; }
        DateTime Timestamp { get; }
    }
}
