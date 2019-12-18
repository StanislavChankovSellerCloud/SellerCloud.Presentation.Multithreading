using System.Threading.Tasks;

namespace SellerCloud.DotNetFramework.Service
{
    public class DotNetFrameworkService
    {
        public async Task ExecuteNothingAsync(bool configAwait)
        {
            await Task.Delay(1000).ConfigureAwait(configAwait);
        }
    }
}
