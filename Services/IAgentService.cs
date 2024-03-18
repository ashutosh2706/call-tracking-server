using CallServer.Dto;
using CallServer.Models;

namespace CallServer.Services
{
    public interface IAgentService
    {
        Task AcquireAgentAsync(long agentId);
        Task FreeAgentAsync(long agentId);
        Task<IEnumerable<Agent>> GetAllAgentsAsync();
        Task<IEnumerable<Agent>> GetFreeAgentsAsync();
        Task<IEnumerable<AgentResponseDto>> GetAgentResponseDtosAsync();
    }
}