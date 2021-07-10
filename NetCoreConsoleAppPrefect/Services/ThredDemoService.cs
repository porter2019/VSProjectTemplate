using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using $safeprojectname$.IServices;
using $safeprojectname$.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace $safeprojectname$.Services
{
    /// <summary>
    /// 多线程演示
    /// </summary>
    public class ThredDemoService : BaseBusinessService, IThredDemoService
    {
        /// <summary>
        /// 产生随机数
        /// </summary>
        private Random _random;

        public ThredDemoService(ILogger<ThredDemoService> logger, IConfiguration config) : base(logger, config)
        {
            _random = new Random();
        }

        /// <summary>
        /// 主方法
        /// </summary>
        /// <returns></returns>
        public Task Exec()
        {
            //最大子线程数量
            var maxThread = _config.GetValue<int>("MultipleThread:MaxThread");
            //线程池设置
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(maxThread, maxThread);

            //监控子线程是否完成，不能超过64个，不然报错：The number of WaitHandles must be less than or equal to 64
            List<ManualResetEvent> threadDoneWatchList = new();


            for (int i = 1; i <= 20; i++)
            {
                //监控子线程对象
                var threadDoneWatch = new ManualResetEvent(false);
                threadDoneWatchList.Add(threadDoneWatch);

                ThreadPool.QueueUserWorkItem(new WaitCallback(ChildThreadMethod),
                    new TestThreadParams() { Key = $"当前页码：{i}", WatchObject = threadDoneWatch });//构造子线程方法需要的参数

            }

            _logger.LogInformation("主线程执行完毕");

            //等待子线程执行完毕
            WaitHandle.WaitAll(threadDoneWatchList.ToArray());
            _logger.LogInformation("子线程全部执行完成");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 子线程执行
        /// </summary>
        /// <param name="paramsData"></param>
        void ChildThreadMethod(object paramsData)
        {
            TestThreadParams param = (TestThreadParams)paramsData;

            //模仿耗时操作
            Thread.Sleep(_random.Next(500, 2000));

            _logger.LogInformation($"【{Thread.CurrentThread.ManagedThreadId}】子线程执行完毕，参数：" + param.Key);


            //设置该线程已操作完成
            param.WatchObject.Set();
        }

    }
}
