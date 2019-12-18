using SellerCloud.DotNetFramework.Service;
using SellerCloud.NetStandard;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SellerCloud.AspNet.Api.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly DotNetFrameworkService _dotNetFrameworkService;
        private readonly NetStandardService _netStandardService;

        public ValuesController()
        {
            _dotNetFrameworkService = new DotNetFrameworkService();
            _netStandardService = new NetStandardService();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            // What's the value?
            var context = SynchronizationContext.Current;

            return Ok(new { SynchronizationContext = context });
        }

        [HttpGet]
        [Route("api/values/net-standard/async")]
        public async Task<IHttpActionResult> AsyncStandard([FromUri] bool configAwait = false)
        {
            await _netStandardService.ExecuteNothingAsync(configAwait);

            return Ok($"NetStandard-Asynchronous-ConfigureAwait={configAwait}");
        }

        [HttpGet]
        [Route("api/values/net-standard/sync")]
        public IHttpActionResult SyncStandard([FromUri] bool configAwait = false)
        {
            _netStandardService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            return Ok(Result.Success(""));
        }

        [HttpGet]
        [Route("api/values/full-framework/async")]
        public async Task<IHttpActionResult> AsyncFramework([FromUri] bool configAwait = false)
        {
            await _dotNetFrameworkService.ExecuteNothingAsync(configAwait);

            return Ok($"FullFramework-Asynchronous-ConfigureAwait={configAwait}");
        }

        [HttpGet]
        [Route("api/values/full-framework/sync")]
        public IHttpActionResult SyncFramework([FromUri] bool configAwait = false)
        {
            _dotNetFrameworkService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            return Ok($"FullFramework-Synchronous-ConfigureAwait={configAwait}");
        }
    }
}
