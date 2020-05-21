using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using NetFamre.server;
using NetFamre.auto;
using GameProtocol;
using GameProtocol.dto;
namespace MyServer.Logic.impl
{
    public class FIghtHandler : ABSSendHandler,IFight
    {

        IBizFight bizFight = BizFightFactory.bizFigth;
        Random random = new Random();
        public void ClientConnect(UserToken token)
        {
            throw new NotImplementedException();
        }

        public void CloseClient(UserToken token, string Message)
        {
            Console.WriteLine(Message);
            bizFight.OffLine(token);
        }
        public void MessageReceive(UserToken token, SocketModel model)
        {
            switch (model.command)
            {
                case FightProtocol.Fight_REQS:
                    //Console.WriteLine(model.message);
                    FightDis(token,model.GetMessage<int>());
                    break;
                case FightProtocol.CallFight_REQS:
                    CallFightDis(token, model.GetMessage<int>());
                    break;
                case FightProtocol.PlayerMove_REQS:
                    Move(model.GetMessage<AccountDTO>(),FightProtocol.PlayerMove_ANS,token);
                    break;
                case FightProtocol.BallMove_REQS:
                    Move(model.GetMessage<AccountDTO>(), FightProtocol.BallMove_ANS, token);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 移动处理
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="protocol"></param>
        void Move(AccountDTO dto,byte protocol,UserToken userToken)
        {
            tool.ExecutorTool.instance.Execute(delegate ()
            {
                UserToken token=bizFight.PlayerBallMove(dto.rivaID);
                if (token != null)
                {
                    if (protocol == FightProtocol.BallMove_ANS)
                    {
                        Write(token, Protocol.FIGHT, 0, protocol, dto);
                        Write(userToken, Protocol.FIGHT, 0, protocol, dto);
                    }
                    else
                    {
                        Write(token, Protocol.FIGHT, 0, protocol, dto);
                    }
                }
            }
            );          
        }       
        void FightDis(UserToken token,int id)
        {
            tool.ExecutorTool.instance.Execute
                (
                delegate ()
                {                  
                    List<int> list = bizFight.FindFight(token, id);
                    if (id == 0)
                    {
                        Write(token, Protocol.FIGHT, 0, FightProtocol.Fight_ANS, bizFight.GetID(token).ToString());
                    }
                    while (list.Count >= 2)
                    {
                        int count = random.Next(2);
                        if (count == 0)
                        {
                            BallAllot(ref list, 0, 1);
                        }
                        else
                        {
                            BallAllot(ref list, 1, 0);
                        }
                    }
                }
                );
           
        }
        void BallAllot(ref List<int> list,int kicpOne,int kicpTwo)
        {
            UserToken userToken = bizFight.GetUserToken(list[0]);
            string str = list[1] + ","+  kicpOne;
            Write(userToken, Protocol.FIGHT, 0, FightProtocol.Fight_ANS, str);
            UserToken userToken2 = bizFight.GetUserToken(list[1]);
            string str2 = list[0] + ","+ kicpTwo;
            Write(userToken2, Protocol.FIGHT, 0, FightProtocol.Fight_ANS, str2);
            bizFight.AddDicRoom(list[0], list[1]);
            bizFight.CallFight(list[0]);
            bizFight.CallFight(list[0]);
        }
        void CallFightDis(UserToken token,int id)
        {
            tool.ExecutorTool.instance.Execute
                (
                delegate () 
                {
                    bizFight.CallFight(id);
                }
                );          
        }
    }
}
