using System;
using System.Collections.Generic;

namespace CallServer.Models;

public partial class CallDetail
{
    public string CallId { get; set; } = null!;

    public long? Hid { get; set; }

    public long? AgentId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual Agent? Agent { get; set; }

    public virtual Hospital? HidNavigation { get; set; }
}
