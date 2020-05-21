using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace MyServer.tool
{
    public delegate void ExecutorDele();
    /// <summary>
    /// 多线程控制，确保一个线程执行完，另一个线程才能进入
    /// </summary>
    public class ExecutorTool
    {
        #region 单例
        private static ExecutorTool _instance;
        public static ExecutorTool instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExecutorTool();
                }
                return _instance;
            }
        }
        #endregion

        Mutex mutex = new Mutex();
        public void Execute(ExecutorDele d)
        {
            lock (this)
            {
                mutex.WaitOne();
                d();
                mutex.ReleaseMutex();
            }
        }
    }
}
