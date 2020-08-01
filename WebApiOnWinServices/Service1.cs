using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public partial class Service1 : ServiceBase
    {

        Logger log = LogManager.GetCurrentClassLogger();
        IDisposable apiserver = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartWebAPI();

        }

        protected override void OnStop()
        {
            StopWebAPI();
        }


        #region 私有方法

        void StartWebAPI()
        {
            var serveruri = "http://+:" + AppSettingConfig.WebApiPort + "/";
            try
            {
                apiserver = WebApp.Start<Startup>(url: serveruri);
                log.Info($"服务已启动: http://127.0.0.1:{AppSettingConfig.WebApiPort}/swagger");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        void StopWebAPI()
        {
            if (apiserver != null) apiserver.Dispose();
            
            log.Info("服务已停止");
        }

        #endregion

    }
}
