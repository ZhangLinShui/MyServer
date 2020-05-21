using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.biz
{
    public class BizFightFactory
    {
        public static readonly IBizFight bizFigth;
        static BizFightFactory()
        {
            bizFigth = new impl.ReFight();
        }
    }
}
