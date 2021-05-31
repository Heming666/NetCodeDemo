using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Util.Log
{
    public class WriteLog
    {
        private static BlockingCollection<LogItem> logList = new BlockingCollection<LogItem>();
        static WriteLog()
        {
            Task.Run(() =>
            {
                foreach (var item in logList.GetConsumingEnumerable())
                {
                    try
                    {
                        string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", item.Type);
                        if (!Directory.Exists(dirPath))
                            Directory.CreateDirectory(dirPath);
                        string logpath = Path.Combine(dirPath, $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
                        if (!System.IO.File.Exists(logpath))
                        {
                            var file = System.IO.File.Create(logpath);
                            file.Close();
                        }
                        string content = string.Format("{0} {1} \r\n", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), item.Msg);
                        content += "------------------------------------------------------------\r\n";
                        System.IO.File.AppendAllText(logpath, content);

                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1000);
                        logList.Add(item);
                    }
                }
            });
        }

        public static void AddLog(string str, string  fileName= "log")
        {
            logList.Add(new LogItem { Msg = str, Type = fileName });
        }
    }

    public class LogItem
    {
        public LogItem()
        {
            _createTime = DateTime.Now;
        }


        private DateTime _createTime;
        public DateTime CreateTime
        {
            get { return _createTime; }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; ; }
        }


        private string _msg;

        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

    }
}