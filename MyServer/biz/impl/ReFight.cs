using NetFamre.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.Cache;
using GameProtocol.dto;

namespace MyServer.biz.impl
{
    public class ReFight : IBizFight
    {
        ICacheFight cacheFight = FightFactory.cacheFight;
        int id;   
        /// <summary>
        /// 玩家和小球移动
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public UserToken PlayerBallMove(int ID)
        {
            return cacheFight.Get(ID);
        }
        /// <summary>
        /// 开启对局
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void AddDicRoom(int id, int value)
        {
            Console.WriteLine("有两个玩家开启了对局，ID号分别是：{0}  VS  {1}",id,value);
            cacheFight.AddDicRoom(id, value);
        }
        /// <summary>
        /// 获取对应的ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetID(UserToken token)
        {
           return cacheFight.GetID(token);
        }
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        public void OffLine(UserToken token)
        {
            cacheFight.OffLine(token);
        }
        /// <summary>
        /// 获取对应的UserToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserToken GetUserToken(int id)
        {
            return cacheFight.Get(id);
        }

        /// <summary>
        /// 取消对局
        /// </summary>
        /// <param name="id"></param>
        public void CallFight(int id)
        {          
            cacheFight.ReMovePlayer(id);
        }
        /// <summary>
        /// 寻找对局
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<int> FindFight(UserToken token, int id)
        {       
            List<int> list = new List<int>();
            if (id == 0)
            {
                this.id++;
                cacheFight.AddID(this.id, token);
                list = cacheFight.AddPlayer(this.id);
            }
            else
            {
                list=cacheFight.AddPlayer(id);
            }
            return list;
        }      
    }
}
