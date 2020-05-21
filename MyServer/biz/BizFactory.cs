using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.biz
{
    public class BizFactory
    {
        public static readonly IEBiz logic;
        static BizFactory()
        {
            logic = new impl.ReBiz();
        }      
    }
}
