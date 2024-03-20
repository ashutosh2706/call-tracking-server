using CallServer.Services;
using CallServer.Util;
using Microsoft.AspNetCore.SignalR;

namespace CallServer.Hubs
{
    public class CallHub : Hub
    {
        private readonly IHospitalService _hospitalService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<Dashboard> _dashboardContext;
        public CallHub(IHospitalService hospitalService, IHttpContextAccessor httpContextAccessor, IHubContext<Dashboard> dashboardContext)
        {
            _hospitalService = hospitalService;
            _httpContextAccessor = httpContextAccessor;
            _dashboardContext = dashboardContext;

        }
        public override async Task OnConnectedAsync()
        {
            var connectiondId = Context.ConnectionId;
            Console.WriteLine($"Connected [{connectiondId}]");
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null )
            {
                if(httpContext.Request.Query.TryGetValue("hid", out var hid))
                {
                    long hospitalId;
                    long.TryParse( hid, out hospitalId);

                    if(await _hospitalService.ConnectAgentAsync(hospitalId, connectiondId))
                    {
                        await Clients.Client(connectiondId).SendAsync("Connected");
                    }
                    else
                    {
                        WaitingQueue.AddWaitingCall(connectiondId, hospitalId);
                        await Clients.Client(connectiondId).SendAsync("Waiting");
                        await _dashboardContext.Clients.All.SendAsync("QueueUpdate", WaitingQueue.WaitingCount(1), WaitingQueue.WaitingCount(2));
                    }

                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var connectionId = Context.ConnectionId;
            await _hospitalService.DisconnectAgentAsync(connectionId);
            Console.WriteLine($"Disconnected [{connectionId}]");

            var waitingCall = WaitingQueue.DispatchWaitingCall();
            if(waitingCall.Item2 != -1)
            {
                string connection = waitingCall.Item1;
                long hospitalId = waitingCall.Item2;
                if(await _hospitalService.ConnectAgentAsync(hospitalId, connection))
                {
                    await Clients.Client(connection).SendAsync("Connected");
                }
            }

            await _dashboardContext.Clients.All.SendAsync("QueueUpdate", WaitingQueue.WaitingCount(1), WaitingQueue.WaitingCount(2));

        }


    }
}
