using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetFamre.server
{
    /// <summary>
    /// 用户工具类
    /// </summary>
    public class UserTokenTool
    {
        /// <summary>
        /// 堆栈，存储每个连接的客户端Socket
        /// </summary>
        private Stack<UserToken> tool;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxNum">表示最大可连接数</param>
        public UserTokenTool(int maxNum)
        {
            tool = new Stack<UserToken>(maxNum);//实例化堆栈
        }
        /// <summary>
        /// 取出顶上的位置，用于存储已连接Socket
        /// </summary>
        /// <returns></returns>
        public UserToken Pop()
        {
            return tool.Pop();
        }
        /// <summary>
        /// 将一个新的连接存储进堆栈当中
        /// </summary>
        /// <param name="token"></param>
        public void Push(UserToken token)
        {
            tool.Push(token);
        }
        /// <summary>
        /// 获取堆栈的大小
        /// </summary>
        public int Size
        {
            get
            {
                return tool.Count;
            }
        }
    }
}
