using NetCoreConsoleAppPrefect.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

namespace NetCoreConsoleAppPrefect.Services
{
    /// <summary>
    /// 非数据库相关的业务基类
    /// </summary>
    [ServiceLifetime(false)]
    public class BaseBusinessService : IBaseBusinessService
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        protected ILogger _logger;

        /// <summary>
        /// 配置文件
        /// </summary>
        protected IConfiguration _config;

        /// <summary>
        /// 程序根目录
        /// </summary>
        protected readonly string _appBaseDirectory;

        public BaseBusinessService(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _appBaseDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }

    }
}
