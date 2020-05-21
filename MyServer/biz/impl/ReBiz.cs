using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
using MyServer.Cache;
namespace MyServer.biz.impl
{
    public class ReBiz : IEBiz
    {
        IECache logic = CacheFactory.logic;
        public int Create(UserToken token, string account, string password)
        {
            if(logic.hasAccount(account))
            {
                return -1;
            }
            logic.Add(account, password);
            return 0;
        }

        public void OffLine(UserToken token)
        {
            logic.OffLine(token);
        }

        public int Online(UserToken token, string account, string password)
        {
            if (!logic.hasAccount(account))
            {
                return -1;
            }
            if(!logic.isRight(account,password))
            {
                return -2;
            }
            if(logic.isOnLine(account))
            {
                return -3;
            }
            logic.Online(token, account);
            return 0;
        }
    }
}
