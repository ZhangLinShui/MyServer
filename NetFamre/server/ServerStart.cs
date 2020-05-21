using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NetFamre.auto;
namespace NetFamre.server
{
    /// <summary>
    /// 服务器开启
    /// </summary>
    public class ServerStart
    {
        Socket server;//创建Socket
        int maxNum;//最大上线人数
        Semaphore semaphore;//信号量
        UserTokenTool tool;//用户管理工具类
        LengthEncode LE;//定义粘包处理的委托
        LengthDecode LD;//定义分包处理的委托
        Encode encode;//定义消息体序列化的委托
        Decode decode;//定义消息体反序列化的委托
        public ABSHandlerCenter center;//定义一个消息控制中心类
        /// <summary>
        /// 构造函数，函数最大上线人数
        /// </summary>
        /// <param name="maxNum"></param>
        public ServerStart(int maxNum)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//实例化服务器        
            this.maxNum = maxNum;//最大上线人数
            semaphore = new Semaphore(maxNum, maxNum);//初始化信号量
            tool = new UserTokenTool(maxNum);//初始化用户管理工具类     
            LE = LengthCoding.EnCode;//将粘包处理操作给委托
            LD = LengthCoding.DeCode;//将分包处理操作给委托
            encode = MessageCoding.encode;//将消息体序列化的方法给委托
            decode = MessageCoding.decode;//将消息体反序列化的方法给委托
        }
        public void Start(int port)
        {
            for (int i = 0; i < maxNum; i++)
            {
                UserToken token = new UserToken();//将每个token放进堆栈中，相当于预先确定了最大在线人数，既最多可连的客户端数
                token.receiveSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);//当异步挂起时，完成会自动执行添加的接收方法
                token.sendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);//当异步挂起时，完成会自动执行添加的发送方法
                token.LE = LE;
                token.LD = LD;
                token.encode = encode;
                token.decode = decode;
                token.center = center;
                token.sendDele = ProcessSend;
                token.closeDele = ClientClose;
                tool.Push(token);
            }
            server.Bind(new IPEndPoint(IPAddress.Any, port));//绑定
            server.Listen(20);//监听用户连接
            StartAccept(null);//调用高级连接的方法
            #region 老方法
            //socket.BeginAccept(AsynAccept,socket);//创建一个异步的连接    
            #endregion
        }

        /// <summary>
        /// 高级方法开启连接
        /// </summary>
        /// <param name="e"></param>
        #region 老方法
        //byte[] bufferRe = new byte[1024]; 
        #endregion
        void StartAccept(SocketAsyncEventArgs e)
        {
            if (e == null)
            {
                e = new SocketAsyncEventArgs();//初始化 如果想使用SocketAsyncEventArgs必须给他的Completed添加方法
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);//当异步挂起时，完成会自动执行添加的方法
            }
            else
            {
                e.AcceptSocket = null;
            }
            semaphore.WaitOne();//信号量-1
            bool result = server.AcceptAsync(e);//判断是否是异步完成
            if (!result)//当他是同步完成时，不会执行 e.Completed，我们需要手动调用
            {
                ProcessAccept(e);
            }
            #region 老方法
            //Socket socket = ar.AsyncState as Socket;
            //Socket clientSocket=socket.EndAccept(ar);
            //Console.WriteLine("有一个客户端连接");
            //Console.WriteLine(clientSocket.LocalEndPoint);
            //clientSocket.BeginReceive(bufferRe, 0, bufferRe.Length,SocketFlags.None,AsynReceive,clientSocket);
            //socket.BeginAccept(AsynAccept, socket);         
            #endregion
        }
        /// <summary>
        /// 中间者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }
        /// <summary>
        /// 执行连接操作
        /// </summary>
        /// <param name="e"></param>
        void ProcessAccept(SocketAsyncEventArgs e)
        {
            UserToken token = tool.Pop();//拿出一个用户对象使用
            token.conn = e.AcceptSocket;
            //TODO..通知应用层有新用户连接
            Console.WriteLine("一个客户端连接！");
            StartRecive(token.receiveSAEA);//开始接受信息
            StartAccept(e);//调用连接的方法
        }
        /// <summary>
        /// 开启接收方法
        /// </summary>
        /// <param name="e"></param>
         void StartRecive(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;//将userToken 转换为用户对象
            bool result = token.conn.ReceiveAsync(e);//判断是否是异步接收，如果不是则要手动调用接收的方法
            if (!result)
            {
                ProcessRecive(e);
            }
        }
        /// <summary>
        /// 确定操作类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessRecive(e);
            }
            else
            {
                ProcessSend(e);
            }
        }
        /// <summary>
        /// 执行接收的方法
        /// </summary>
        /// <param name="e"></param>
         void ProcessRecive(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;//把对象转化出来
            if (token.receiveSAEA.BytesTransferred > 0 && token.receiveSAEA.SocketError == SocketError.Success)
            {
                byte[] result = new byte[token.receiveSAEA.BytesTransferred];
                Buffer.BlockCopy(token.receiveSAEA.Buffer, 0, result, 0, result.Length);
                token.Receive(result);
                StartRecive(e);
            }
            else
            {            
                if (token.receiveSAEA.SocketError != SocketError.Success)
                {
                    ClientClose(token, token.receiveSAEA.SocketError.ToString());
                }
                else
                {
                    ClientClose(token, "客户端主动断开连接");
                }
            }
        }
        /// <summary>
        /// 执行发送的方法
        /// </summary>
        /// <param name="e"></param>
         void ProcessSend(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;//把对象转化出来
            if (token.sendSAEA.SocketError == SocketError.Success)
            {
                token.Send();//回调
            }
            else
            {
                ClientClose(token, token.sendSAEA.SocketError.ToString());
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OffMessage"></param>
         void ClientClose(UserToken token, string OffMessage)
        {
            if (token.conn != null)
            {
                lock (token)
                {
                    //通知应用层，有客户端断开连接
                    center.CloseClient(token, OffMessage);
                    token.Close();
                    tool.Push(token);
                    semaphore.Release();//信号量加1
                }
            }
        }
        #region 老方法

        //public void AsynReceive(IAsyncResult ar)
        //{

        //    Socket clientSocket = ar.AsyncState as Socket;
        //    int count = clientSocket.EndReceive(ar);
        //    string message = Encoding.UTF8.GetString(bufferRe, 0, count);
        //    Console.WriteLine(message);
        //    clientSocket.BeginReceive(bufferRe, 0, bufferRe.Length, SocketFlags.None, AsynReceive, clientSocket);

        //} 
        #endregion
    }
}
