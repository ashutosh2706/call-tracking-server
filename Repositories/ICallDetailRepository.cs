using CallServer.Models;

namespace CallServer.Repositories
{
    public interface ICallDetailRepository
    {
        Task AddCallDetailAsync(CallDetail callDetail);
        Task<IEnumerable<CallDetail>> GetAllCallDetailsAsync();
        Task<CallDetail?> GetCallDetailByIdAsync(string callId);
        Task UpdateCallDetailAsync(CallDetail callDetail);
    }
}