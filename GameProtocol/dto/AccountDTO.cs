using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.dto
{
    /// <summary>
    /// 账户模型
    /// </summary>
    [Serializable]
    public class AccountDTO
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string passwrod { get; set; }
        /// <summary>
        /// 自己ID
        /// </summary>
        public int seltID { get; set; }
        /// <summary>
        /// 对手ID
        /// </summary>
        public int rivaID { get; set; }        
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string message;
    }
}

