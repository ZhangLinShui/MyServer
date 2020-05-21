using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
using NetFamre.auto;
using GameProtocol;
using GameProtocol.dto;
using MyServer.biz;
namespace MyServer.Logic.impl
{
    public class LoginHandler : ABSSendHandler, ABSHandlerCenter
    {
        IEBiz logic = BizFactory.logic;//业务逻辑层
        public void ClientConnect(UserToken token)
        {
            throw new NotImplementedException();
        }

        public void CloseClient(UserToken token, string Message)
        {
            Console.WriteLine(Message);
            logic.OffLine(token);
        }

        public void MessageReceive(UserToken token, SocketModel model)
        {
            switch (model.command)
            {
                case LoginRegistProtocol.REG_REQS:
                    Regist(token,model.GetMessage<AccountDTO>());
                    break;
                case LoginRegistProtocol.Login_REQS:
                    Login(token, model.GetMessage<AccountDTO>());
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 登陆处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dto"></param>
      void Login(UserToken token,AccountDTO dto)
        {
            tool.ExecutorTool.instance.Execute(delegate () 
            {
                int message = logic.Online(token, dto.account, dto.passwrod);//调用业务逻辑层的登陆方法
                Write(token, Protocol.LOGINREGIST, 0, LoginRegistProtocol.Login_ANS, message);//将结果返回给客户端
            });
         }
        /// <summary>
        /// 注册处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dto"></param>
        void Regist(UserToken token, AccountDTO dto)
        {
            tool.ExecutorTool.instance.Execute(delegate ()
            {
            int message = logic.Create(token, dto.account, dto.passwrod);//调用业务逻辑层的注册方法
            Write(token, Protocol.LOGINREGIST, 0, LoginRegistProtocol.REG_ANS, message);//将结果返回给客户端
            });          
        }
    }
}
