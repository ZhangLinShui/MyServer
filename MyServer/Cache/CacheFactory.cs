using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Cache
{
    public class CacheFactory
    {
        public static readonly IECache logic;
        static CacheFactory()
        {
            logic = new impl.ReCache();
        }
    }
}
