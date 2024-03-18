using CallServer.Services;
using CallServer.Util;
using Microsoft.AspNetCore.SignalR;


namespace CallServer.Hubs
{
    public class Hospital_1 : Hub
    {

        private readonly IHospitalService _hospitalService;
        private readonly IHubContext<Hospital_2> _hospital_2;
        private readonly IHubContext<Dashboard> _dashboard;

        public Hospital_1(IHospitalService hospitalService, IHubContext<Hospital_2> hospital_2, IHubContext<Dashboard> dashboard)
        {
            _hospitalService = hospitalService;
            _hospital_2 = hospital_2;
            _dashboard = dashboard;
        }

        public override async Task OnConnectedAsync()
        {
            var channelId = Guid.NewGuid().ToString();
            var connectionId = Context.ConnectionId;

            /*
             * Add the new connection to a group which represent a channel. 
             * client connectionId and Agent connectionId will be both added to same channel
             * This ensures message sent by client will be sent in a channel with agent connectionId in that channel
             * Method: Clients.Group(channel).SendAsync(receiver_func(), message);
             * 
             */
            await Groups.AddToGroupAsync(connectionId, channelId);
            Context.Items.Add(connectionId, channelId);

            /* 
             * .SendAsync(...) calls the client side method and doesn't wait for response
             * .InvokeAsync(...) calls the client side method and wait for the response
             */
            
            if(await _hospitalService.ConnectAgentAsync(1, channelId))
            {
                await Clients.Client(connectionId).SendAsync("Connected", channelId);
            }
            else
            {
                WaitingQueue.AddWaitingCall(connectionId, channelId, 1);
                await Clients.Client(connectionId).SendAsync("Waiting");
                int h1 = WaitingQueue.WaitingForHospital_1, h2 = WaitingQueue.WaitingForHospital_2;
                await _dashboard.Clients.All.SendAsync("QueueUpdate", h1, h2);
            }
            
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var channelId = "";
            channelId = Context.Items[connectionId].ToString();
            await Groups.RemoveFromGroupAsync(connectionId, channelId);
            await _hospitalService.DisconnectAgentAsync(channelId);

            var waitingCall = WaitingQueue.DispatchWaitingCall();

            if(waitingCall.Item3 != -1)
            {

                string channel_Id = waitingCall.Item2;
                long hospital_Id = waitingCall.Item3;

                if (hospital_Id == 1)
                {

                    if (await _hospitalService.ConnectAgentAsync(1, channel_Id))
                    {
                        await Clients.Group(channel_Id).SendAsync("Connected", channel_Id);
                    }
                } 
                else
                {
                    if (await _hospitalService.ConnectAgentAsync(2, channel_Id))
                    {
                        await _hospital_2.Clients.Group(channel_Id).SendAsync("Connected", channel_Id);
                    }
                }
            }

            int h1 = WaitingQueue.WaitingForHospital_1, h2 = WaitingQueue.WaitingForHospital_2;
            await _dashboard.Clients.All.SendAsync("QueueUpdate", h1, h2);

        }


        public async Task Transfer(string channelId, string message)
        {
            await Clients.Group(channelId).SendAsync("ReceiveMessage", message);
        }


    }
}
