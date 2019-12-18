using Microsoft.AspNetCore.Mvc;
using SellerCloud.DotNetFramework.Service;
using SellerCloud.NetStandard;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.AspNetCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DotNetFrameworkService _dotNetFrameworkService;
        private readonly NetStandardService _netStandardService;

        public ValuesController()
        {
            _dotNetFrameworkService = new DotNetFrameworkService();
            _netStandardService = new NetStandardService();
        }

        [HttpGet]
        public IActionResult GetSynchronizationContext()
        {
            // What's the value?
            var context = SynchronizationContext.Current;

            return Ok(context);
        }

        [HttpGet("net-standard/async")]
        public async Task<IActionResult> AsyncStandard([FromQuery] bool configAwait = false)
        {
            await _netStandardService.ExecuteNothingAsync(configAwait);

            return Ok($"NetStandard-Asynchronous-ConfigureAwait={configAwait}");
        }

        [HttpGet("net-standard/sync")]
        public IActionResult SyncStandard([FromQuery] bool configAwait = false)
        {
            _netStandardService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            return Ok($"NetStandard-Synchronous-ConfigureAwait={configAwait}");
        }

        [HttpGet("full-framework/async")]
        public async Task<IActionResult> AsyncFramework([FromQuery] bool configAwait = false)
        {
            await _dotNetFrameworkService.ExecuteNothingAsync(configAwait);

            return Ok($"FullFramework-Asynchronous-ConfigureAwait={configAwait}");
        }

        [HttpGet("full-framework/sync")]
        public IActionResult SyncFramework([FromQuery] bool configAwait = false)
        {
            _dotNetFrameworkService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            return Ok($"FullFramework-Synchronous-ConfigureAwait={configAwait}");
        }
    }
}
