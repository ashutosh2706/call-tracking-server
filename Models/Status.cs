using System;
using System.Collections.Generic;

namespace CallServer.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
