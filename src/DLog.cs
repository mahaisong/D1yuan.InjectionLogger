using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;


namespace D1yuan.InjectionLogger
{
    public class DLog : IDisposable
    {
        #region 单例

        private static object lockobj = new object();

        private static DLog _instance;

        public static DLog Instance
        {

            get
            {
                if (null == _instance)
                {
                    lock (lockobj)
                    {
                        if (null == _instance)
                        {
                            _instance = new DLog();
                        }


                    }
                }
                return _instance;

            }

        }

        #endregion


        private const int _maxQueuedMessages = 1073741824;

        private readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(_maxQueuedMessages);

        private readonly Thread _outputThread;

        private string _folderPath = string.Empty;

        private byte[] line = new byte[1024];
        public DLog()
        {
            _folderPath = Path.Combine(System.Environment.CurrentDirectory, "InjectionLogger");
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            line = Encoding.UTF8.GetBytes("\r\n");
            // Start Console message queue processor
            _outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "logger queue processing thread"
            };
            _outputThread.Start();
        }


        private string _curFileName = string.Empty;
        private string _filePath = string.Empty;
        // for testing
        internal virtual void WriteMessage(string entry)
        {
            var bytes = Encoding.UTF8.GetBytes(entry);
            var dateStr = DateTime.Now.ToString("yyyyMMdd");
            if (String.Compare(_curFileName, dateStr) != 0)
            {
                _curFileName = dateStr;
                _filePath = Path.Combine(_folderPath, $"{_curFileName}.txt");
                if (!File.Exists(_filePath))
                {
                    using (FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(line);
                        fs.Write(bytes);

                    }

                }

            }
            using (FileStream fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
            {
                fs.Write(line);
                fs.Write(bytes);
            }
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
                catch (InvalidOperationException) { }
            }

            // Adding is completed so just log the message
            try
            {
                WriteMessage(message);
            }
            catch (Exception) { }
        }



        private void ProcessLogQueue()
        {
            try
            {
                foreach (string message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
            catch
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch { }
            }
        }

        public void Dispose()
        {
            _messageQueue.CompleteAdding();

            try
            {
                _outputThread.Join(1500); // with timeout in-case Console is locked by user input
            }
            catch (ThreadStateException) { }
        }
    }
}
