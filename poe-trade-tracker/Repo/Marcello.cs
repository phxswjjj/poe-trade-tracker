using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Repo
{
    public class Marcello
    {
        public static MarcelloDB.Session CreateSession()
        {
            var platform = new MarcelloDB.netfx.Platform();
            var session = new MarcelloDB.Session(platform, @".");
            return session;
        }
    }
}
