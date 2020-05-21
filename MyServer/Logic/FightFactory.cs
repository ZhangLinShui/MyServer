using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Logic
{
    public class FightFactory
    {
        public static readonly IFight Fight;
        static FightFactory()
        {
            Fight =new impl.FIghtHandler();
        }
    }
}
