using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol
{
    public class FightProtocol
    {
        /// <summary>
        /// 客户端请求对战
        /// </summary>
        public const byte Fight_REQS = 4;
        /// <summary>
        /// 服务器响应客户端的对战请求
        /// </summary>
        public const byte Fight_ANS = 5;

        /// <summary>
        /// 客户端取消对战请求
        /// </summary>
        public const byte CallFight_REQS = 6;
        /// <summary>
        /// 服务器响应客户端的取消对战请求
        /// </summary>
        public const byte CallFight_ANS = 7;

        /// <summary>
        /// 客户端玩家移动请求
        /// </summary>
        public const byte PlayerMove_REQS = 8;
        /// <summary>
        /// 服务器响应客户端的玩家移动请求
        /// </summary>
        public const byte PlayerMove_ANS = 9;

        /// <summary>
        /// 客户端小球移动请求
        /// </summary>
        public const byte BallMove_REQS = 10;
        /// <summary>
        /// 服务器响应客户端的小球移动请求
        /// </summary>
        public const byte BallMove_ANS = 11;
    }
}
