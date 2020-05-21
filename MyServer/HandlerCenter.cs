using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
using MyServer.Logic;
using NetFamre.auto;
using GameProtocol;
namespace MyServer
{
    /// <summary>
    /// 消息控制中心
    /// </summary>
    public class HandlerCenter : ABSHandlerCenter
    {
        ABSHandlerCenter logic = LogicFactory.Logic;//逻辑层
        IFight fight = FightFactory.Fight;//寻找对战逻辑层    
        /// <summary>
        /// 用户连接
        /// </summary>
        /// <param name="token"></param>
        public void ClientConnect(UserToken token)
        {

        }
        /// <summary>
        /// 用户断开连接  
        /// </summary>
        /// <param name="token"></param>
        /// <param name="MessageBuffer"></param>
        public void CloseClient(UserToken token, string Message)
        {
            fight.CloseClient(token, Message);
        }
        /// <summary>
        /// 管理消息接受
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        public void MessageReceive(UserToken token, SocketModel model)
        {
            switch (model.Type)
            {
                case Protocol.LOGINREGIST:
                    logic.MessageReceive(token,model);
                    Console.WriteLine(model.message);
                    break;
                case Protocol.FIGHT:
                    fight.MessageReceive(token, model);
                    break;              
                default:
                    break;
            }
        }
    }
}
