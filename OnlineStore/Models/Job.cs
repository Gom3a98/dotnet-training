using System;
using System.Collections.Generic;

namespace OnlineStore.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string JobDesc { get; set; } = null!;
}
