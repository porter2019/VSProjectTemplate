using System.Threading.Tasks;

namespace NetCoreConsoleAppPrefect.IServices
{
    public interface IDemoService : IBaseBusinessService
    {
        Task Test();
    }
}
