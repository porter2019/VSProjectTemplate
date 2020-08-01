using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController: ApiController
    {
        protected Logger log = LogManager.GetCurrentClassLogger();

        protected int GlobalUserId { get; set; }


        public void OnActionExecuting(HttpActionContext actionContext)
        {
            GlobalUserId = 1;
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }
}
