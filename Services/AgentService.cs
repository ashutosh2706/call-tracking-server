using CallServer.Dto;
using CallServer.Models;
using CallServer.Repositories;

namespace CallServer.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IStatusService _statusService;
        public AgentService(IAgentRepository agentRepository, IStatusService statusService)
        {
            _agentRepository = agentRepository;
            _statusService = statusService;
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
        {
            return await _agentRepository.GetAllAgentsAsync();
        }



        public async Task<IEnumerable<Agent>> GetFreeAgentsAsync()
        {
            // 1 = Free, 2 = Busy, 3 = Leave
            List<Agent> freeAgents = new List<Agent>();
            var agents = await _agentRepository.GetAllAgentsAsync();
            foreach (var agent in agents)
            {
                if (agent.StatusId == 1)
                {
                    freeAgents.Add(agent);
                }
            }
            return freeAgents;

        }

        public async Task AcquireAgentAsync(long agentId)
        {
            var agent = await _agentRepository.GetAgentByIdAsync(agentId);
            if(agent != null)
            {
                agent.StatusId = 2;
                await _agentRepository.UpdateAgentAsync(agent);
            }
        }

        public async Task FreeAgentAsync(long agentId)
        {
            var agent = await _agentRepository.GetAgentByIdAsync(agentId);
            if (agent != null)
            {
                agent.StatusId = 1;
                await _agentRepository.UpdateAgentAsync(agent);
            }
        }

        public async Task<IEnumerable<AgentResponseDto>> GetAgentResponseDtosAsync()
        {
            var agents = await _agentRepository.GetAllAgentsAsync();
            List<AgentResponseDto> responseDtos = new List<AgentResponseDto>();
            var statuses = await _statusService.GetAllStatusAsync();
            Dictionary<int, String> statusCodeDescription = new Dictionary<int, String>();
            foreach(var status in statuses)
            {
                if(status.Description != null)
                    statusCodeDescription.Add(status.StatusId, status.Description);
            }
            foreach (var agent in agents)
            {
                var responseDto = new AgentResponseDto
                {
                    AgentId = agent.AgentId,
                    AgentName = agent.Name,
                    Status = statusCodeDescription[agent.StatusId ?? -1]
                };
                responseDtos.Add(responseDto);
            }

            return responseDtos;
        }

    }
}
