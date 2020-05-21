using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using NetFamre.auto;
namespace NetFamre.server
{
    /// <summary>
    /// 单个客户端类
    /// </summary>
    public class UserToken
    {
        public Socket conn;//用于连接的Socket 
        public SocketAsyncEventArgs receiveSAEA;//用于接收的网络流
        public SocketAsyncEventArgs sendSAEA;//用于发送的网络流
        private List<byte> cache = new List<byte>();//消息缓存
        private Queue<byte[]> queue = new Queue<byte[]>(); //发送队列
        public ABSHandlerCenter center;//定义一个消息控制中心类
        public LengthEncode LE;//定义粘包处理的委托
        public LengthDecode LD;//定义分包处理的委托
        public Encode encode;//定义消息体序列化的委托
        public Decode decode;//定义消息体反序列化的委托
        public ProcessSendDele sendDele;//定义发送的委托
        public ClientCloseDele closeDele;//定义关闭的委托
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserToken()
        {
            receiveSAEA = new SocketAsyncEventArgs();//初始化
            sendSAEA = new SocketAsyncEventArgs();//初始化
            receiveSAEA.UserToken = this;//将自己绑定
            sendSAEA.UserToken = this;//将自己绑定
            receiveSAEA.SetBuffer(new byte[1024], 0, 1024);//设置接收流缓冲区的大小
        }
        /// <summary>
        /// 是否能接收
        /// </summary>
        private bool isReading = true;
        /// <summary>
        /// 是否能发送
        /// </summary>
        private bool isWriting = true;
        /// <summary>
        /// 接收的方法
        /// </summary>
        /// <param name="result"></param>
        public void Receive(byte[] result)
        {
            cache.AddRange(result);
            if (isReading)
            {
                isReading = false;
                OnRead();
            }
        }

        /// <summary>
        /// 不停接收，直到消息缓存cache为空
        /// </summary>
        public void OnRead()
        {
            byte[] buffer = null;
            if (LD != null)//假如存在分包现象
            {
                buffer = LD(ref cache);//执行分包
                if (buffer == null)//说明数据还不足以解析
                {
                    isReading = true;
                    return;
                }             
            }
            else
            {
                if (cache.Count == 0)
                {
                    isReading = true;
                    return;
                }
                else
                {
                    buffer = cache.ToArray();
                    cache.Clear();
                }
            }
            if (decode == null)
            {
                throw new Exception("没有进行消息反序列化的工具");//抛出异常和return的作用           
            }
            SocketModel model = decode(buffer);          
            center.MessageReceive(this, model); //处理数据
            OnRead();
        }
        /// <summary>
        /// 发送
        /// </summary>
        public void Send()
        {
            OnWrite();
        }
        /// <summary>
        /// 不停发送
        /// </summary>
        public void OnWrite()
        {
            if (queue.Count == 0)
            {
                isWriting = true;
                return;
            }
            byte[] buffer = queue.Dequeue();
            sendSAEA.SetBuffer(buffer, 0, buffer.Length);
            bool result = conn.SendAsync(sendSAEA);
            if (!result)
            {
                sendDele(sendSAEA);
            }
        }
        /// <summary>
        /// 发送的方法
        /// </summary>
        /// <param name="message"></param>
        public void Writed(byte[] message)
        {
            if (conn == null)
            {
                closeDele(this, "客户端断开了连接");
                return;
            }
            if (encode == null)
            {
                throw new Exception("没有添加序列化的方法");
            }
            queue.Enqueue(message);
            if (isWriting)
            {
                isWriting = false;
                OnWrite();
            }
        }
        /// <summary>
        /// 连接断开，关闭
        /// </summary>
        public void Close()
        {
            cache.Clear();
            queue.Clear();
            isReading = true;
            isWriting = true;
            conn.Shutdown(SocketShutdown.Both);
            conn = null;
        }
    }
}
