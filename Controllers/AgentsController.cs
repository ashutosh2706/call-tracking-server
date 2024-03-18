using CallServer.Dto;
using CallServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallServer.Controllers
{
    [Route("/api")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;
        private readonly IStatusService _statusService;
        public AgentsController(IAgentService agentService, IStatusService statusService)
        {
            _agentService = agentService;
            _statusService = statusService;
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAllAgents()
        {
            try
            {
                var response = await _agentService.GetAgentResponseDtosAsync();
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
