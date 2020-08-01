using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    /// <summary>
    /// AppSeetting配置数据
    /// </summary>
    public class AppSettingConfig
    {
        /// <summary>
        /// API端口
        /// </summary>
        public static readonly string WebApiPort = ConfigurationManager.AppSettings["WebApiPort"];



    }
}
