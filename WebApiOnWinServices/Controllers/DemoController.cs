using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// 演示
    /// </summary>
    [RoutePrefix("api/demo")]
    public class DemoController : BaseController
    {
        /// <summary>
        /// 测试API服务是否正常启动
        /// </summary>
        /// <returns></returns>
        [Route("get")]
        [HttpGet]
        public ApiResult Get()
        {
            return ApiResult.OK(new Models.Demo() { id = 1, title = "接口已通" });
        }
    }
}
