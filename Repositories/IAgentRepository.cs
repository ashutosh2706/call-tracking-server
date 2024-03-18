using CallServer.Models;

namespace CallServer.Repositories
{
    public interface IAgentRepository
    {
        Task<Agent?> GetAgentByIdAsync(long agentId);
        Task<IEnumerable<Agent>> GetAllAgentsAsync();
        Task UpdateAgentAsync(Agent agent);
    }
}