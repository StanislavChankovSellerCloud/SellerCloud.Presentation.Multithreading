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

            var result = new RestResult<SynchronizationContext>()
            {
                Value = context,
            };

            return Ok(result);
        }

        [HttpGet("net-standard/async")]
        public async Task<IActionResult> AsyncStandard([FromQuery] bool configAwait = false)
        {
            await _netStandardService.ExecuteNothingAsync(configAwait);

            string message = $"NetStandard-Asynchronous-ConfigureAwait={configAwait}";

            var result = RestResult<object>.Empty(message);

            return Ok(result);
        }

        [HttpGet("net-standard/sync")]
        public IActionResult SyncStandard([FromQuery] bool configAwait = false)
        {
            _netStandardService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            string message = $"NetStandard-Synchronous-ConfigureAwait={configAwait}";

            var result = RestResult<object>.Empty(message);

            return Ok(result);
        }

        [HttpGet("full-framework/async")]
        public async Task<IActionResult> AsyncFramework([FromQuery] bool configAwait = false)
        {
            await _dotNetFrameworkService.ExecuteNothingAsync(configAwait);

            string message = $"FullFramework-Asynchronous-ConfigureAwait={configAwait}";

            var result = RestResult<object>.Empty(message);

            return Ok(result);
        }

        [HttpGet("full-framework/sync")]
        public IActionResult SyncFramework([FromQuery] bool configAwait = false)
        {
            _dotNetFrameworkService.ExecuteNothingAsync(configAwait).GetAwaiter().GetResult();

            string message = $"FullFramework-Synchronous-ConfigureAwait={configAwait}";

            var result = RestResult<object>.Empty(message);

            return Ok(result);
        }
    }
}
