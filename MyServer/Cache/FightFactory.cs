using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Cache
{
    public class FightFactory
    {
        public static readonly ICacheFight cacheFight;
        static FightFactory()
        {
            cacheFight = new impl.CacheFight();
        }
    }
}
