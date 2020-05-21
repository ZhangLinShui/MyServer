using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetFamre.auto;
namespace NetFamre.server
{
    /// <summary>
    /// 消息控制中心接口
    /// </summary>
    public interface ABSHandlerCenter
    {
        /// <summary>
        /// 用户连接
        /// </summary>
        void ClientConnect(UserToken token);

        /// <summary>
        ///管理消息接收 
        /// </summary>
        /// <param name="token">接收的客户端</param>
        /// <param name="message">消息体</param>
        void MessageReceive(UserToken token, SocketModel message);

        /// <summary>
        /// 用户断开连接  
        /// </summary>
        /// <param name="token"></param>
        /// <param name="MessageBuffer"></param>
        void CloseClient(UserToken token, string Message);
    }
}
