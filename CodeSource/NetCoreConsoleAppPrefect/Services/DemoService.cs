using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreConsoleAppPrefect.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreConsoleAppPrefect.Services
{
    public class DemoService : BaseBusinessService, IDemoService
    {
        public DemoService(ILogger<DemoService> logger, IConfiguration config) : base(logger, config)
        {

        }

        public Task Test()
        {
            _logger.LogInformation(_config.GetValue<string>("Default:Key") + "--" + _config.GetValue<decimal>("Default:Key2"));

            return Task.CompletedTask;
        }
    }
}
