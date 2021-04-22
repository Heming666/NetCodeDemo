using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Log
{
    public class LoggerEntity
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogInfo { get; set; }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// url地址
        /// </summary>
        public string  Url { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
    }
}
