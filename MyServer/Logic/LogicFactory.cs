using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
namespace MyServer.Logic
{
    public class LogicFactory
    {
        public static readonly ABSHandlerCenter Logic;
        static LogicFactory()
        {
            Logic = new impl.LoginHandler();
        }
    }
}
