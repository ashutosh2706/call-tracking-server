using CallServer.Models;

namespace CallServer.Services
{
    public interface ICallDetailService
    {
        Task AddCallDetailAsync(string callId, long agentId, long hospitalId);
        Task<IEnumerable<CallDetail>> GetAllCallDetailsAsync();
        Task<CallDetail?> GetCallDetailByIdAsync(string callId);
        Task UpdateCallDetailAsync(string callId);
    }
}