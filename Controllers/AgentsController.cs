using CallServer.Dto;
using CallServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;
        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var response = await _agentService.GetAgentResponseDtosAsync();
            return Ok(response);
        }
    }
}
