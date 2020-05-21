using NetFamre.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Cache.impl
{
    public class CacheFight : ICacheFight
    {
        public Dictionary<int, UserToken> dicPlayer = new Dictionary<int, UserToken>();//在线玩家
        List<int> fightPlayer = new List<int>();//寻找对局玩家
        Dictionary<int, int> dicRoom = new Dictionary<int, int>();//正在对局的玩家

        public void AddDicRoom(int id,int value)
        {
            dicRoom.Add(id, value);
        }
        public void RemoveDicRoom(int id)
        {
            Console.WriteLine("一位玩家强行关闭游戏，退出了对局");
            foreach (var item in dicRoom.Keys)
            {
                if (item == id || dicRoom[item] == id)
                {
                    if (item == id)
                    {
                        dicRoom.Remove(id);
                    }
                    else
                    {
                        dicRoom.Remove(item);
                    }
                    break;
                }
            }
        }
        public int GetID(UserToken token)
        {
            int ID=0;
            foreach (var item in dicPlayer.Keys)
            {
                if (dicPlayer[item] == token)
                {
                    ID = item;
                    break;
                }
            }
            return ID;
        }
        public void OffLine(UserToken token)
        {
            foreach (var item in dicPlayer.Keys)
            {
                if (dicPlayer[item] == token)
                {
                    dicPlayer.Remove(item);
                    if (dicRoom.ContainsKey(item) || dicRoom.ContainsValue(item))
                    {
                        RemoveDicRoom(item);
                    }
                    if(fightPlayer.Contains(item))
                    {
                        ReMovePlayer(item);
                    }
                    break;
                }
            }
        }
        public UserToken Get(int id)
        {
            if(!dicPlayer.ContainsKey(id))
            {
                return null;
            }
            return dicPlayer[id];
        }
        public void AddID(int id,UserToken token)
        {
            dicPlayer.Add(id, token);
        }

        public void RemoveID(int id)
        {
            dicPlayer.Remove(id);
        }
        public List<int> AddPlayer(int id)
        {
            if(fightPlayer.Contains(id))
            {
                Console.WriteLine("该玩家已在寻找对局队列中");
                return fightPlayer;
            }
            Console.WriteLine("一位玩家寻找对局中");
            fightPlayer.Add(id);
            return fightPlayer;
        }
        public void ReMovePlayer(int id)
        {
            Console.WriteLine("一位玩家退出了寻找对局");
            fightPlayer.Remove(id);
        }        

    }
}
