using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobDto
{
    public string GroupName { get; set; }
    public string JobName { get; set; }

    public string JobDescription { get; set; }
    public string TriggerName { get; set; }
    public string TriggerGroupName { get; set; }
    public string TriggerType { get; set; }
    public string TriggerState { get; set; }
    public DateTime PreviousFireTime { get; set; }
    public DateTime NextFireTime { get; internal set; }
    public DateTime StartTime { get; set; }
}

