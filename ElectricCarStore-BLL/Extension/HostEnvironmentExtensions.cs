using Microsoft.Extensions.Hosting;

namespace ElectricCarStore_BLL.Extension
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment("Local");
        }
    }
}
