using CallServer.Hubs;
using CallServer.Models;
using CallServer.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace CallServer.Services
{
    public class CallDetailService : ICallDetailService
    {

        private readonly ICallDetailRepository _callDetailRepository;
        
        public CallDetailService(ICallDetailRepository callDetailRepository)
        {
            _callDetailRepository = callDetailRepository;
        }

        public async Task<IEnumerable<CallDetail>> GetAllCallDetailsAsync()
        {
            return await _callDetailRepository.GetAllCallDetailsAsync();
        }

        public async Task<CallDetail?> GetCallDetailByIdAsync(string callId)
        {
            return await _callDetailRepository.GetCallDetailByIdAsync(callId);
        }

        public async Task AddCallDetailAsync(string callId, long agentId, long hospitalId)
        {
            CallDetail callDetail = new CallDetail
            {
                CallId = callId,
                AgentId = agentId,
                Hid = hospitalId,
                StartTime = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay)
            };

            await _callDetailRepository.AddCallDetailAsync(callDetail);
        }

        public async Task UpdateCallDetailAsync(string callId)
        {

            var callDetail = await _callDetailRepository.GetCallDetailByIdAsync(callId);
            if(callDetail != null)
            {
                callDetail.EndTime = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay);
                await _callDetailRepository.UpdateCallDetailAsync(callDetail);
            }
        }
    }
}
