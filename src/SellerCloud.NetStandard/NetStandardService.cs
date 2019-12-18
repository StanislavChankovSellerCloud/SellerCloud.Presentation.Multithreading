using System.Threading.Tasks;

namespace SellerCloud.NetStandard
{
    public class NetStandardService
    {
        public async Task ExecuteNothingAsync(bool configAwait)
        {
            await Task.Delay(1000).ConfigureAwait(configAwait);
        }
    }
}
