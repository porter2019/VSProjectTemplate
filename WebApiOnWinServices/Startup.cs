using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace $safeprojectname$
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host.
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" });
            //Swagger 必须
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            //异常统一格式返回
            config.Filters.Add(new WebApiGlobalException());
            config.Filters.Add(new TimingActionFilter());
            config.EnableCors(new EnableCorsAttribute("*", "*", "*")); //允许跨域
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
            SwaggerConfig.Register(config);
            app.UseWebApi(config);
        }
    }

    /// <summary>
    /// 更改返回的数据类型为Json
    /// </summary>
    class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
            return result;
        }
    }

    /// <summary>
    /// 全局异常处理
    /// </summary>
    class WebApiGlobalException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger log = LogManager.GetCurrentClassLogger();

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, ApiResult.Error(actionExecutedContext.Exception.Message));
            log.Error(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }

    class TimingActionFilter : ActionFilterAttribute
    {
        Logger log = LogManager.GetCurrentClassLogger();

        private const string Key = "__action_duration__";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (SkipLogging(actionContext))
            {
                return;
            }
            var stopWatch = new Stopwatch();
            actionContext.Request.Properties[Key] = stopWatch;
            stopWatch.Start();

            //if (!(actionContext.ControllerContext.Controller is Controllers.BaseController))
            //{
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ApiResult.Failed($"访问地址[{actionContext.Request.RequestUri.PathAndQuery}]不是一个有效的WebApi"));
            //    return;
            //}

            //Controllers.BaseController apiController = (Controllers.BaseController)actionContext.ControllerContext.Controller;

            ////调用基类里的
            //apiController.OnActionExecuting(actionContext);

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Request.Properties.ContainsKey(Key))
            {
                return;
            }

            var stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;
            if (stopWatch != null)
            {
                stopWatch.Stop();
                log.Debug($"请求:{actionExecutedContext.Request.RequestUri.PathAndQuery},耗时:{stopWatch.ElapsedMilliseconds}ms");
            }

        }

        private static bool SkipLogging(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<NoLogAttribute>().Any() ||
                    actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<NoLogAttribute>().Any();
        }
    }

    /// <summary>
    /// 无需记录耗时日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogAttribute : Attribute
    {

    }
}
