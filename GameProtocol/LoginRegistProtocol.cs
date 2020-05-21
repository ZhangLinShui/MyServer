using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol
{
    public class LoginRegistProtocol
    {
        /// <summary>
        /// 客户端请求登录
        /// </summary>
        public const byte Login_REQS = 0;
        /// <summary>
        /// 服务器响应客户端的登录请求
        /// </summary>
        public const byte Login_ANS = 1;

        /// <summary>
        /// 客户端请求注册
        /// </summary>
        public const byte REG_REQS = 2;
        /// <summary>
        /// 服务器响应客户端的注册请求
        /// </summary>
        public const byte REG_ANS = 3;
    }
}
