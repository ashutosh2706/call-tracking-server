
namespace CallServer.Services
{
    public interface IHospitalService
    {
        Task<bool> ConnectAgentAsync(long hospitalId, string channelId);
        Task DisconnectAgentAsync(string channelId);
    }
}