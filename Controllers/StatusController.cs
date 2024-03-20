using CallServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var response = await _statusService.GetStatusResponseDtosAsync();
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddNewStatus(string statusDescription)
        {
            var status = await _statusService.AddStatusAsync(statusDescription);
            return Created("/Status", status);
        }
    }
}
