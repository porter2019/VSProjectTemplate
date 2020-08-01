using System.Linq;
using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using $safeprojectname$;


[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace $safeprojectname$
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "");
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            }).EnableSwaggerUi();
        }
        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\Swagger.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
