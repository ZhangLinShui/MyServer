using NetFamre.auto;
using NetFamre.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Logic
{
    public interface IFight
    {        
        void MessageReceive(UserToken token, SocketModel model);
        void ClientConnect(UserToken token);
        void CloseClient(UserToken token, string Message);
    }
}
