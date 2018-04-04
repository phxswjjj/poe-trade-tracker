﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader
{
    interface ILoader
    {
        void Reload();

        void SetBlacklist(IEnumerable<string> blacklists);
    }
}
