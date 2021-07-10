using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreConsoleAppPrefect.Model
{
    /// <summary>
    /// 多线程方法参数基类
    /// </summary>
    public class BaseMultiThreadParams
    {
        /// <summary>
        /// 通过ManualResetEvent来通知子线程执行完毕
        /// </summary>
        public ManualResetEvent WatchObject { get; set; }
    }
}
