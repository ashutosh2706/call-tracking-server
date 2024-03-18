using CallServer.Dto;
using CallServer.Hubs;
using CallServer.Models;
using CallServer.Repositories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CallServer.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IAgentService _agentService;
        private readonly ICallDetailService _callDetailService;
        private readonly IStatusService _statusService;
        private readonly IHubContext<Dashboard> _dashboard;


        public HospitalService(IHospitalRepository hospitalRepository, IAgentService agentService, ICallDetailService callDetailService, IStatusService statusService, IHubContext<Dashboard> dashboard)
        {
            _hospitalRepository = hospitalRepository;
            _agentService = agentService;
            _callDetailService = callDetailService;
            _dashboard = dashboard;
            _statusService = statusService;

        }

        public async Task<bool> ConnectAgentAsync(long hospitalId, string channelId)
        {
            // list of all agents in that hospital
            var agents = await _hospitalRepository.GetAgentsByHospitalIdAsync(hospitalId);
            foreach (var agent in agents)
            {
                if (agent.StatusId == 1)
                {
                    await _agentService.AcquireAgentAsync(agent.AgentId);
                    /* call_service will make new record with call_id=channel_id
                     * so that when client disconnect call, channel_id will be used to identify associated agent 
                     * and agent can be freed and further processing can be done on that call
                     */
                    await _callDetailService.AddCallDetailAsync(channelId, agent.AgentId, hospitalId);

                    var allAgents = await _agentService.GetAllAgentsAsync();
                    List<Object> result = new List<Object>();
                    foreach(var agent1  in allAgents)
                    {
                        
                        var obj = new { agentId = agent1.AgentId, agentName = agent1.Name, status = agent1.StatusId };
                        result.Add(obj);
                    }
                    await _dashboard.Clients.All.SendAsync("Update", JsonConvert.SerializeObject(result));
                    return true;
                }
            }

            /* no agents are free, try again by calling this method again
             * save the channelId, connection Id in a Q.when a call is disonnected, check Q for waiting calls and serve them.
             * this will be done in FCFS order, both hospitals have separate Q
             */
            return false;
        }

        public async Task DisconnectAgentAsync(string channelId)
        {
            var callDetail = await _callDetailService.GetCallDetailByIdAsync(channelId);
            if(callDetail != null)
            {
                await _callDetailService.UpdateCallDetailAsync(channelId);
                await _agentService.FreeAgentAsync(callDetail.AgentId.GetValueOrDefault());
                var allAgents = await _agentService.GetAllAgentsAsync();
                List<Object> result = new List<Object>();
                foreach (var agent1 in allAgents)
                {
                    var obj = new { agentId = agent1.AgentId, agentName = agent1.Name, status = agent1.StatusId };
                    result.Add(obj);
                }
                await _dashboard.Clients.All.SendAsync("Update", JsonConvert.SerializeObject(result));
            }
        }
    }
}
