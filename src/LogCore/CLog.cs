using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;


namespace D1yuan.InjectionLogger
{
    /// <summary>
    /// Console打印日志
    /// </summary>
    public class CLog : IDisposable, ID1Log
    {
        #region 单例

        private static object lockobj = new object();

        private static CLog _instance;

        public static CLog Instance
        {

            get
            {
                if (null == _instance)
                {
                    lock (lockobj)
                    {
                        if (null == _instance)
                        {
                            _instance = new CLog();
                        }


                    }
                }
                return _instance;

            }

        }

        public bool IsEnable { get; set; } = false;

        #endregion


        private const int _maxQueuedMessages = 1073741824;

        private readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(_maxQueuedMessages);

        private readonly Thread _outputThread;
 
        public CLog()
        { 
            // Start Console message queue processor
            _outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "Console logger queue processing thread"
            };
            _outputThread.Start();
        }

         
        // for testing
        internal virtual void WriteMessage(string entry)
        { 
            Console.WriteLine(entry); 
        }


        public virtual void EnqueueMessage(string message)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                try
                {
                    _messageQueue.Add(message);
                    return;
                }
                catch (Exception ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
            }

            // Adding is completed so just log the message
            try
            {
                WriteMessage(message);
            }
            catch (InvalidOperationException ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
        }



        private void ProcessLogQueue()
        {
            try
            {
                //不需要线程sleep。
                //会遍历集合取出数据，一旦发现集合空了，则阻塞自己，直到集合中又有元素了再开始遍历。
                foreach (string message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
            catch
            {
                try
                {
                    //不允许任何元素被加入集合；当使用了 CompleteAdding 方法后且集合内没有元素的时候，另一个属性 IsCompleted 此时会为 True，这个属性可以用来判断是否当前集合内的所有元素都被处理完。
                    _messageQueue.CompleteAdding();
                }
                catch (Exception ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
            }
        }

        public void Dispose()
        {
            _messageQueue.CompleteAdding();

            try
            {
                _outputThread.Join(1500); // with timeout in-case Console is locked by user input
            }
            catch (ThreadStateException ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
        }
    }
}
