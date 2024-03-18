using Microsoft.AspNetCore.SignalR;

namespace CallServer.Hubs
{
    public class Dashboard : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Dashboard Connected: [{Context.ConnectionId}]");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Dashboard Disconnected: [{Context.ConnectionId}]");
        }
    }
}
