using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.IServices
{
    /// <summary>
    /// 多线程演示
    /// </summary>
    public interface IThredDemoService : IBaseBusinessService
    {
        Task Exec();
    }
}
