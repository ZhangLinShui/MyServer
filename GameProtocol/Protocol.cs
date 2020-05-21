using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol
{
    public class Protocol
    {
        /// <summary>
        /// 登陆注册类型
        /// </summary>
        public const byte LOGINREGIST= 0;

        /// <summary>
        /// 对战请求类型
        /// </summary>
        public const byte FIGHT = 1;

        /// <summary>
        /// 对战房间
        /// </summary>
        public const byte ROOMFIGHT = 2;
    }
}
