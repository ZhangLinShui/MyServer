using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFamre.server;
namespace MyServer.biz
{
   public interface IEBiz
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="token">上线的客户端</param>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>返回0 上线成功 返回-1账号不存在 -2 密码错误 -3 账号已上线  </returns>
        int Online(UserToken token, string account, string password);


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns>0 成功 -1 不成功 </returns>
        int Create(UserToken token, string account, string password);

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        void OffLine(UserToken token);
    }
}
