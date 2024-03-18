using CallServer.Services;

namespace CallServer.Util
{
    public static class WaitingQueue
    {
        private static Queue<(String, String, long)> _queue = new Queue<(String, String, long)>();
        public static int WaitingForHospital_1 { get; set; } = 0;
        public static int WaitingForHospital_2 { get; set; } = 0;

        // if requirement is like serve calls based on time, then use min-heap with time as first pair, and connectionId as second 

        public static (String, String, long) DispatchWaitingCall()
        {
            if(_queue.Count > 0)
            {
                var tuple = _queue.Dequeue();
                if (tuple.Item3 == 1) WaitingForHospital_1--;
                else WaitingForHospital_2--;

                return tuple;
            } 
            else { return ("","",-1); }
        }

        public static void AddWaitingCall(string connectionId, string channelId, long hospitalId)
        {
            if(hospitalId == 1) WaitingForHospital_1++;
            else WaitingForHospital_2++;

            _queue.Enqueue((connectionId, channelId, hospitalId));
        }
    }
}