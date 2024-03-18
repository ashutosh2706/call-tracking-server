using System;
using System.Collections.Generic;

namespace CallServer.Models;

public partial class Agent
{
    public long AgentId { get; set; }

    public string Name { get; set; } = null!;

    public int? StatusId { get; set; }

    public virtual ICollection<CallDetail> CallDetails { get; set; } = new List<CallDetail>();

    public virtual Status? Status { get; set; }

    public virtual ICollection<Hospital> Hids { get; set; } = new List<Hospital>();
}
