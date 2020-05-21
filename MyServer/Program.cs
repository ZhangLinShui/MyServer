using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerStart server = new ServerStart(1000);
            server.center = new HandlerCenter();
            server.Start(8080);
           Console.ReadKey();
        }
    }
}
