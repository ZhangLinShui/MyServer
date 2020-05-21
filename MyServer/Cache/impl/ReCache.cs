using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
using MyServer.Model;
namespace MyServer.Cache.impl
{
    public class ReCache : IECache
    {
        int id = 0;
        //上线玩家userToken 和ID绑定
        Dictionary<UserToken, string> onLineMap = new Dictionary<UserToken, string>();

        //注册的账号和账号模型绑定
        Dictionary<string, AccountModel> allUserMap = new Dictionary<string, AccountModel>();

        public void Add(string account, string password)
        {
            allUserMap.Add(account, new AccountModel(account, password));
        }

        public bool hasAccount(string account)
        {
           if(allUserMap.ContainsKey(account))
            {
                return true;
            }
            return false;
        }

        public bool isOnLine(string account)
        {
            foreach (var item in onLineMap.Keys)
            {
                if(onLineMap[item].Equals(account))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isRight(string account, string password)
        {
            return allUserMap[account].password.Equals(password);
        }

        public void OffLine(UserToken token)
        {
            onLineMap.Remove(token);      
        }

        public void Online(UserToken token, string account)
        {
            onLineMap.Add(token, account);
        }
    }
}
