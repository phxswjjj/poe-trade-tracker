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
            if(display.ItemCount > 0)
                this.ItemName = display.ItemName;
        }
        public string Url { get; set; }
        public string ItemName { get; set; }
    }
}
