using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * 使用Mustachio模板引擎自动生成
 */

namespace NetCoreConsoleAppPrefect.IServices
{
    /// <summary>
    /// 代码生成服务
    /// </summary>
    public interface ICodeGenerateService : IBaseBusinessService
    {
        /// <summary>
        /// 生成通用业务服务文件
        /// </summary>
        /// <param name="serviceName">服务类名</param>
        /// <param name="serviceDesc">服务说明</param>
        void GenerateBusinessServiceFile(string serviceName,string serviceDesc);
    }
}
