using System;
using System.Collections.Generic;

namespace CallServer.Models;

public partial class Hospital
{
    public long Hid { get; set; }

    public string Hname { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<CallDetail> CallDetails { get; set; } = new List<CallDetail>();

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
