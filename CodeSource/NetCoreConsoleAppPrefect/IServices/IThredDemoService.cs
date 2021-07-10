using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreConsoleAppPrefect.IServices
{
    /// <summary>
    /// 多线程演示
    /// </summary>
    public interface IThredDemoService
    {
        Task Exec();
    }
}
