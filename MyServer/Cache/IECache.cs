using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
namespace MyServer.Cache
{
   public interface IECache
    {
        /// <summary>
        /// 判断字典是否有这个账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool hasAccount(string account);
        /// <summary>
        /// 判断是否在线上
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool isOnLine(string account);
        /// <summary>
        /// 判断账号密码是否正确
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool isRight(string account, string password);

        /// <summary>
        /// 上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        void Online(UserToken token, string account);
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        void OffLine(UserToken token);
        /// <summary>
        /// 添加新账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        void Add(string account, string password);
    }
}
