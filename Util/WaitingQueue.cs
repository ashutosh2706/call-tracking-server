namespace CallServer.Util
{
    public static class WaitingQueue
    {
        private static readonly int totalHospitals = 2;

        private static Queue<(String, long)> _queue = new Queue<(String, long)>();

        private static List<int> waitingCounts = Enumerable.Repeat(0, totalHospitals+1).ToList();

        public static (String, long) DispatchWaitingCall()
        {
            if(_queue.Count > 0)
            {
                var tuple = _queue.Dequeue();
                waitingCounts[(int)tuple.Item2]--;
                return tuple;
            } 
            else { return ("",-1); }
        }

        public static void AddWaitingCall(string connectionId, long hospitalId)
        {
            waitingCounts[(int)hospitalId] += 1;
            _queue.Enqueue((connectionId, hospitalId));
        }

        public static int WaitingCount(long hospitalId)
        {
            return waitingCounts[(int)hospitalId];
        }
    }
}