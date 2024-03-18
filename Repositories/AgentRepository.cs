using CallServer.Data;
using CallServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CallServer.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly CallTrackingDbContext _dbContext;
        public AgentRepository(CallTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
        {
            return await _dbContext.Agents.ToListAsync();
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            _dbContext.Agents.Update(agent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Agent?> GetAgentByIdAsync(long agentId)
        {
            return await _dbContext.Agents.FindAsync(agentId);
        }

    }
}
