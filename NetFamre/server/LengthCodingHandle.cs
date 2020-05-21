using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using NetFamre.auto;
namespace NetFamre.server
{
    /// <summary>
    /// 防止粘包的处理
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public delegate byte[] LengthEncode(byte[] buffer);
    /// <summary>
    /// 分包处理
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public delegate byte[] LengthDecode(ref List<byte> buffer);
    /// <summary>
    /// 消息体的序列化
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public delegate byte[] Encode(SocketModel message);
    /// <summary>
    /// 消息体的反序列化
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public delegate SocketModel Decode(byte[] buffer);
    /// <summary>
    ///关闭的委托
    /// </summary>
    /// <param name="token"></param>
    /// <param name="OffMessage"></param>
    public delegate void ClientCloseDele(UserToken token, string OffMessage);
    /// <summary>
    /// 发送的委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void ProcessSendDele(SocketAsyncEventArgs e);
}
