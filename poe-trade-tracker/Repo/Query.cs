using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Repo
{
    public class Query
    {
        public Query() { }
        public Query(Loader.IGridViewDisplay display)
        {
            this.Url = display.Url;
        }
        public string Url { get; set; }
    }
}
