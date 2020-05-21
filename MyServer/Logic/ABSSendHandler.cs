using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.auto;
using NetFamre.server;
namespace MyServer.Logic
{
    public class ABSSendHandler
    {
        /// <summary>
        /// 发送
        /// </summary>
        public void Write(UserToken token,byte Type,byte area,byte command,object message)
        {
            SocketModel model = new SocketModel();
            model.Type = Type;
            model.Area = area;
            model.command = command;
            model.message = message;
            byte[] result=MessageCoding.encode(model);
            result=LengthCoding.EnCode(result);
            token.Writed(result);
        }
    }
}
